var _EMAIL = '';
var _GRUPOS = [];
var _VOTOS = [];
var _EXIBIR_GRUPOS = false;

var post_loading_y = Number.MAX_VALUE;
var carregando_post = false;
var carregou_todos = false;
var timeline_open = false;

//SECTION
var section_history = [];
var sectionIn = function(section) {	section_history.push(section); };
var sectionOut = function() { section_history.splice(section_history.length - 1); };
window.history.pushState({}, '');
window.addEventListener('popstate', function() {
	window.history.pushState({}, '');
	if (section_history.length > 1) {
		var section = section_history[section_history.length - 1];
		if (section.tipo == "section") sectionClose();
    }
});
var sectionNavigate = function(section, fade) {
	$('html, body').scrollTop(0);
    $('section:not(#' + section.nome + ')').hide();
    if (fade) {
        $('#' + section.nome).stop().fadeIn();
    } else {
        $('#' + section.nome).show();
    }
};

var sectionOpen = function(section) {
	sectionIn({tipo:'section', section:section});
	sectionNavigate(section, true);
};
var sectionClose = function() {
	sectionOut();
	var section = section_history[section_history.length - 1].section;
	sectionNavigate(section, false);
};
var sectionChange = function(section) {
	section_history = [];
	sectionOpen(section);
};



var processPageLoad = function() {
    $(function() {

        var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;

        $('form', '#signup-email').validate({
            submitHandler: function() {
                if (_BLOCK_AJAX) return;
                _BLOCK_AJAX = true;
                $('#signup-email-form .submit').addClass('submit--sending');
                _EMAIL = $('#singup-email-email').val();
                WS.InserirRequisicao('LOGIN', {plataforma:'1', device:'', email:_EMAIL}, function(ret) {
                    _BLOCK_AJAX = false;
                    $('#signup-email-form .submit').removeClass('submit--sending');
                    if (ret.retorno == 1) {
                        if (ret.nome != '') {
                            $('#signup-confirm-nome').val(ret.nome).attr('filled', true);
                        }
                        $('#signup-email').hide();
                        $('#signup-confirm').show();
                    }
                    if (ret.retorno == 2) {
                        toggleAlert('E-mail não autorizado.');
                    }
                    if (ret.retorno == 3) {
                        toggleAlert('Ocorreu um erro ao enviar o e-mail.');
                    }
                });
                return false;
            }
        });
        
        $('form', '#signup-confirm').validate({
            submitHandler: function() {
				if (_BLOCK_AJAX) return;
                _BLOCK_AJAX = true;
                $('#signup-confirm .submit').addClass('submit--sending');
                WS.InserirRequisicao('VALIDA_CODIGO', {
                    email:_EMAIL,
                    codigo:$('#signup-confirm-codigo').val(),
                    nome:$('#signup-confirm-nome').val()
                }, function(ret) {
                    _BLOCK_AJAX = false;
                    $('#signup-confirm .submit').removeClass('submit--sending');
                    if (ret.retorno == 1) {
                        toggleDisclaimer();
                        WS.AtualizaSessao(ret.usuario_id, ret.token);
                        VALIDA_SESSAO('#signup-confirm-form .submit');
                    }
                    if (ret.retorno == 2) {
                        toggleAlert('Código incorreto.');
                    }
                });
                return false;
            }
        });
    
        window.VALIDA_SESSAO = function(submitSelector) {
            if (_BLOCK_AJAX) return;
            _BLOCK_AJAX = true;
            submitSelector && $(submitSelector).addClass('submit--sending');
            WS.InserirRequisicao('VALIDA_SESSAO', {}, function(ret) {
                _BLOCK_AJAX = false;
                submitSelector && $(submitSelector).removeClass('submit--sending');
                if (ret.usuario.PERFIL_COMUM || ret.usuario.PERFIL_MARKETING || ret.usuario.PERFIL_PRESIDENTE || ret.usuario.PERFIL_RH) {
                    $('#bt-novo-post').show();
                    _EXIBIR_GRUPOS = true;
                }
                var html = '<option value=""></option>';
                if (ret.usuario.PERFIL_MARKETING) html += '<option value="1">Marketing</option>';
                if (ret.usuario.PERFIL_RH) html += '<option value="2">RH</option>';
                if (ret.usuario.PERFIL_PRESIDENTE) html += '<option value="3">Presidente</option>';
                if (ret.usuario.PERFIL_COMUM) html += '<option value="4">Em meu nome</option>';
                $('#new-post-name, #new-poll-name').html(html);
    
                _GRUPOS = ret.grupos;
                _VOTOS = ret.votos;
    
                var getCheckGrupos = function(area) {
                    var html = '<div class="label-group">Publicar para os grupos</div>';
                    for (var i = 0; i < _GRUPOS.length; i++) {
                        html += '<div class="check">';
                        html += '   <input id="new-' + area + '-group-' + i + '" type="checkbox" name="group" value="' + _GRUPOS[i].GRUPO_ID + '">';
                        html += '   <label for="new-' + area + '-group-' + i + '" class="label">' + _GRUPOS[i].NOME + '</label>';
                        html += '</div>';
                    }
                    return html;
                };
    
                $('#nova-postagem .field-group').html(getCheckGrupos('post'));
                $('#nova-enquete .field-group').html(getCheckGrupos('poll'));
    
                CARREGAR_TIMELINE();
                SEND_NOTIFICATION_FCM();
            });
        };
    
        window.CARREGAR_TIMELINE = function() {
            $('section').hide();
            // $('#timeline').show();
            sectionChange({nome:'timeline'});
            timeline_open = true;
            CARREGAR_POSTS(0);
        };
    
        window.LoadImages = function() {
            $('.img-load').each(function() {
                if (!$(this).data('loading')) {
                    $(this).data('loading', true);
                    var $img = $(this);
                    loadImage(
                        $img.data('src'),
                        function (img) {
                            if(img.type != "error") {
                                $img.closest('.post-figure').append(img);
                                $img.remove();
                            }
                        },
                        {maxWidth: 343}
                    );
                }
            });
        };

        window.PostParse = function(rPost, withArticle) {
            var data = new Date(parseInt(rPost.DATA_CADASTRO.replace('/Date(', '').replace(')/', '')));
            
            var autor = '';
            if (rPost.PERFIL == 1) autor = 'Marketing';
            if (rPost.PERFIL == 2) autor = 'RH';
            if (rPost.PERFIL == 3) autor = 'Presidência';
            if (rPost.PERFIL == 4) autor = rPost.USUARIO_NOME;
    
            var categoryImg = 'post--text.png';
            if (rPost.IMAGEM != '') categoryImg = 'post--image.png';
            if (rPost.VIDEO != '') categoryImg = 'post--video.png';
    
            var arrGrupos =  rPost.GRUPOS.split('|');
            var arrEIDs = [];
            var arrOpcao = [];
            var arrVotos = [];
            if (rPost.TIPO == 2) {
                arrEIDs = rPost.ENQUETE_IDS.split('|');
                arrOpcao = rPost.ENQUETE_OPCOES.split('|');
                arrVotos = rPost.ENQUETE_VOTOS.split('|');
            }
            var totalVotos = 0;
            var maisVotadoIndex = -1;
            var maisVotadoVotos = -1;
            for (var v = 0; v < arrVotos.length; v++) {
                var opVotos = parseInt(arrVotos[v]);
                if (opVotos > maisVotadoVotos) {
                    maisVotadoVotos = opVotos;
                    maisVotadoIndex = v;
                }
                totalVotos += opVotos;
            }
    
            var html = '';
            if (withArticle) html += '<article class="post post-do-events">';
            if (rPost.TIPO == 1) {
                if (rPost.VIDEO == '') {
                    html += '	<div class="post-category"><img src="img/' + categoryImg + '"></div>';
                }
            }
            if (rPost.VIDEO != '') {
                var cover = '';
                var videoSrc = BACKEND + '/Uploads/Video/' + rPost.VIDEO;
                if (rPost.IMAGEM != '') {
                    cover = ' poster="' + BACKEND + '/Uploads/Imagem/' + rPost.IMAGEM + '"';
                } else {
                    cover = ' poster="' + BACKEND + '/Uploads/Thumb/' + rPost.POST_ID + '.JPG"';
                }
                html += '	<div class="post-figure">';
                //html += '	    <video src="' + videoSrc + '"' + cover + '></video>';
                html += '	    <video controlsList="nodownload"' + cover + '>';
                html += '	        <source src="' + videoSrc + '" type="video/mp4">';
                html += '	    </video>';
                html += '	    <a href="javascript:;" class="post-play">Tocar video</a>';
                html += '	</div>';
            } else {
                if (rPost.IMAGEM != '') {
                    html += '	<div class="post-figure">';
                    //html += '	    <img src="' + BACKEND + '/Uploads/Imagem/' + rPost.IMAGEM + '">';
                    html += '	    <div class="img-load content-placeholder" data-src="' + BACKEND + '/Uploads/Imagem/' + rPost.IMAGEM + '"></div>';
                    html += '	</div>';
                }
            }
            html += '	<div class="post-content">';
            html += '		<div class="post-title">';
            if (rPost.TIPO == 1) {
                html += '			<h3>' + rPost.TITULO + '</h3>';
            } else {
                html += '			<h2>ENQUETE</h2>';
                html += '			<h3>' + rPost.TEXTO + '</h3>';
            }
            html += '		</div>';
            if (rPost.TIPO == 1) {
                html += '		<div class="post-text">';
                html += '			<p>' + rPost.TEXTO.replaceAll('\n', '<br />') + '</p>';
                html += '		</div>';
            }
            if (rPost.PDF != '') {
                html += '		<div class="post-button">';
                html += '			<a href="' + BACKEND + '/Uploads/Pdf/' + rPost.PDF + '" target="_blank">Abrir PDF</a>';
                html += '		</div>';
            }
            
            html += '	</div>';
            if (rPost.TIPO == 2) {
                var votou = false;
                for (var v = 0; v < _VOTOS.length; v++) {
                    if (_VOTOS[v] == rPost.POST_ID) {
                        votou = true;
                        break;
                    }
                }
                html += '	<div class="poll-form">';
                if (!votou) {
                    html += '	    <form>';
                    for (var o = 0; o < arrOpcao.length; o++) {
                        html += '	        <div class="check">';
                        html += '	            <input class="poll-option" id="poll-option-' + rPost.POST_ID + '-' + o + '" type="radio" name="group" value="' + arrEIDs[o] + '" data-rule-require_from_group=\'[1, ".poll-option"]\'>';
                        html += '	            <label for="poll-option-' + rPost.POST_ID + '-' + o + '" class="label">' + arrOpcao[o] + '</label>';
                        html += '	        </div>';
                    }
                    html += '	        <div class="submit">';
                    html += '	            <input type="submit" value="Votar"/>';
                    html += '	        </div>';
                    html += '	    </form>';
                } else {
                    html += '	    <ul class="poll-result"' + ((votou)?' style="display:block"':'') + '>';
                    for (var o = 0; o < arrOpcao.length; o++) {
                        var votoPercent = parseInt(arrVotos[o]) / (totalVotos / 100);
                        html += '	        <li' + ((o == maisVotadoIndex)?' class="poll-voted"':'') +'>';
                        html += '	            <strong>' + arrOpcao[o] + '</strong>';
                        html += '	            <div class="poll-percent" style="width: ' + Math.round(votoPercent) + '%;"></div>';
                        html += '	        </li>';
                    }
                    html += '	    </ul>';
                }
                html += '	</div>';
            }
            html += '	<div class="post-meta">';
            html += '		<div class="post-info">';
            html += '			<div class="post-more"><a href="javascript:;">Ver tudo</a></div>';
            if ((rPost.TIPO == 2) && (totalVotos > 0)) {
                html += '			<div class="post-votes">';
                html += '			    <span>' + totalVotos + '</span> Voto' + ((totalVotos > 1)?'s':'');
                html += '			</div>';
            }
            html += '			<div class="post-date">';
            html += '				<time>' + data.toLocaleDateString('pt-BR') + ' — ' + data.toLocaleTimeString('pt-BR').substr(0, 5) + '</time>';
            html += '			</div>';
            html += '		</div>';
            html += '		<div class="post-labels">';
            html += '			<ul class="post-author">';
            html += '				<li class="ellipsis">' + autor + '</li>';
            html += '			</ul>';
            if (_EXIBIR_GRUPOS) {
                html += '			<ul class="post-tags">';
                for (var g = 0; g < arrGrupos.length; g++) {
                    for (var j = 0; j < _GRUPOS.length; j++) {
                        if (arrGrupos[g] == _GRUPOS[j].GRUPO_ID) {
                            html += '<li>' + _GRUPOS[j].NOME + '</li>';
                            break;
                        }
                    }
                }
                html += '			</ul>';
            }
            html += '		</div>';
            html += '	</div>';
            if (withArticle) html += '</article>';
            return html;
        };
    
        window.PostEventos = function() {
            $('.post-do-events').each(function() {
                if ($(this).find('.post-text p').height() > 150) {
                    $(this).find('.post-more').show();
                }
                $(this).find('.poll-form form').validate({
                    submitHandler: function(form) {
                        var $form = $(form);
                        if ($form.find('input[type=radio]:checked').length == 0) {
                            toggleAlert('Escolha uma opção.');
                            return;
                        }
        
                        var opcao_id = $form.find('input[type=radio]:checked').val();
        
                        if (_BLOCK_AJAX) return;
                        _BLOCK_AJAX = true;
                        $form.find('.submit').addClass('submit--sending');
        
                        WS.InserirRequisicao('ENQUETE_VOTAR', {OPCAO_ID: opcao_id}, function(ret) {
                            _BLOCK_AJAX = false;
                            $form.find('.submit').removeClass('submit--sending');
                            if (ret.retorno == 1) {
                                _VOTOS[_VOTOS.length] = ret.post.POST_ID;
                                $(form).closest('article').html(window.PostParse(ret.post, false));
                                window.LoadImages();
                            }
                            if (ret.retorno == 2) {
                                toggleAlert('Você já respondeu esta enquete.');
                            }
                            if (ret.retorno == 3) {
                                toggleAlert('Opção não encontrada.');
                            }
                        });
        
                        return false;
                    }
                });
            });
            $('.post-do-events').removeClass('post-do-events');
        };
    
        var _LAST_POST_ID = 0;
        var _POSTS_POR_LOAD = 4;
        window.CARREGAR_POSTS = function(last_post_id) {
            if (carregando_post) return false;
            carregando_post = true;
            var loading_post_html = '<article class="post post-loading"><div class="post-category content-placeholder avatar-placeholder"></div><div class="post-content"><div class="post-title content-placeholder title-placeholder"></div><div class="text-body"><div class="content-placeholder text-placeholder"></div><div class="content-placeholder text-placeholder"></div><div class="content-placeholder text-placeholder"></div></div></div><div class="post-meta"><div class="post-info"><div class="post-more content-placeholder more-placeholder"></div><div class="post-date content-placeholder date-placeholder"></div></div><div class="post-labels"><ul class="post-author content-placeholder author-placeholder"></ul></div></div></article>';
            if (last_post_id == 0) {
                if ($('#bx-splash').is(':visible')) {
                    $('#bx-splash').fadeOut();
                    $('#main').show();
                }
                $('#timeline-wrapper').html(loading_post_html + loading_post_html + loading_post_html + loading_post_html + loading_post_html);
                $(window).scrollTop(0);
                carregou_todos = false;
            }
            WS.InserirRequisicao('GET_POSTS', {last_post_id:last_post_id}, function(ret) {
                if (ret.posts.length > 0) {
                    var html = '';
                    for (var i = 0; i < ret.posts.length; i++) {
                        html += window.PostParse(ret.posts[i], true);
                    }
                    html += loading_post_html;
                    $('#timeline-wrapper .post-loading').remove();
                    $('#timeline-wrapper').append(html);
                    window.LoadImages();
                    window.PostEventos();
                    
                    var $last_loading = $('#timeline-wrapper .post-loading:last');
                    post_loading_y = $last_loading.offset().top - window.innerHeight + ($last_loading.height() / 2);
        
                    _LAST_POST_ID = ret.posts[ret.posts.length - 1].POST_ID;
                    
                } else {
                    carregou_todos = true;
                    $('#timeline-wrapper .post-loading').slideUp();
                    var html = '<article class="advise"><figure><img src="img/advise-ok.png"><figcaption>VOCÊ CHEGOU NO FINAL!</figcaption></figure></article>';
                    $('#timeline-wrapper').append(html);
                }
    
                carregando_post = false;
                
            });
        };
    
        $(window).scroll(function() {
            if ((!carregou_todos) && (!carregando_post) && (window.scrollY > post_loading_y)) {
                CARREGAR_POSTS(_LAST_POST_ID);
            }
        });
    
        $('form', '#nova-postagem').validate({
            submitHandler: function() {
                var arrGrupos = [];
                $('#nova-postagem input[name="group"]:checked').each(function() {
                    if ($(this).val() != '0') arrGrupos[arrGrupos.length] = $(this).val();
                });
    
                if (arrGrupos.length == 0) {
                    toggleAlert('Selecione pelo menos um grupo.');
                    return;
                }
                
                if (_BLOCK_AJAX) return;
                _BLOCK_AJAX = true;
                $('#nova-postagem-form .submit').addClass('submit--sending');
    
                var imagem = '';
                var video = '';
                var pdf = '';
    
                var Send_Novo_Post = function() {
                    WS.InserirRequisicao('NOVO_POST', {
                        TIPO: 1, //1 - Conteudo, 2 - Enquete
                        PERFIL: $('#new-post-name').val(),
                        GRUPOS: arrGrupos,
                        IMAGEM: imagem,
                        VIDEO: video,
                        PDF: pdf,
                        TITULO: $('#new-post-title').val(),
                        TEXTO: $('#new-post-text').val(),
                    }, function(ret) {
                        _BLOCK_AJAX = false;
                        $('#nova-postagem-form .submit').removeClass('submit--sending');
                        if (ret.retorno == 1) {
                            resetForms();
                            CARREGAR_TIMELINE();
                        }
                        if (ret.retorno == 2) {
                            toggleAlert('Dados Inválidos.');
                        }
                        if (ret.retorno == 3) {
                            toggleAlert('Usuário sem permissão.');
                        }
                    });
                };
    
                var done = 0;
    
                var processaUpload = function(id, nome) {
                    if ($(id)[0].files.length > 0) {
                        done++;
                        var formData = new FormData();
                        formData.append('ACAO', 'UPLOAD_' + nome);
                        formData.append('WS_USUARIO_ID', WS_USUARIO_ID);
                        formData.append('WS_TOKEN', WS_TOKEN);
                        formData.append(nome, $(id)[0].files[0]);
                        var uploadSucesso = false;
                        $.ajax({
                            url: WS_HTTP_URL,
                            data: formData,
                            type: 'POST',
                            contentType: false,
                            processData: false,
                            success: function(data) {
                                var ret = JSON.parse(data);
                                if (ret.sucesso) {
                                    done--;
                                    if (nome == 'IMAGEM') imagem = ret.ext;
                                    if (nome == 'VIDEO') video = ret.ext;
                                    if (nome == 'PDF') pdf = ret.ext;
                                    if (done == 0) Send_Novo_Post();
                                } else {
                                    if (ret.motivo == "SESSAO_INVALIDA") {
                                        WS.Logout();
                                    } 
                                    else if (ret.motivo == "FORMATO_INCORRETO") {
                                        $('#nova-postagem-form .submit').removeClass('submit--sending');
                                        toggleAlert('Formato do arquivo inválido.');
                                        _BLOCK_AJAX = false;
                                    }
                                    else {
                                        $('#nova-postagem-form .submit').removeClass('submit--sending');
                                        toggleAlert('Desculpe, ocorreu um erro ao fazer Upload.');
                                        _BLOCK_AJAX = false;
                                    }
                                }
                                uploadSucesso = true;
                            },
                            complete: function(XMLHttpRequest, textStatus) {
                                console.log('complete');
                                if (!uploadSucesso) {
                                    toggleAlert('Desculpe, ocorreu um erro ao fazer Upload.');
                                    _BLOCK_AJAX = false;
                                }
                            }
                        });
                    }
                };
                processaUpload('#new-post-image', 'IMAGEM');
                processaUpload('#new-post-video', 'VIDEO');
                processaUpload('#new-post-pdf', 'PDF');
                
                if (done == 0) Send_Novo_Post();
    
                return false;
            }
        });
    
        $('form', '#nova-enquete').validate({
            submitHandler: function() {
                var arrGrupos = [];
                $('#nova-enquete input[name="group"]:checked').each(function() {
                    if ($(this).val() != '0') arrGrupos[arrGrupos.length] = $(this).val();
                });
                if (arrGrupos.length == 0) {
                    toggleAlert('Selecione pelo menos um grupo.');
                    return;
                }
    
                var arrOpcao = [];
                $('#nova-enquete .field-multiple input').each(function() {
                    if ($(this).val() != '')  arrOpcao[arrOpcao.length] = $(this).val();
                });            
                if (arrOpcao.length < 2) {
                    toggleAlert('Preencha pelo menos duas opções.');
                    return;
                }
                
                if (_BLOCK_AJAX) return;
                _BLOCK_AJAX = true;
                $('#nova-enquete-form .submit').addClass('submit--sending');
    
                WS.InserirRequisicao('NOVO_POST', {
                    TIPO: 2, //1 - Conteudo, 2 - Enquete
                    PERFIL: $('#new-poll-name').val(),
                    GRUPOS: arrGrupos,
                    IMAGEM: '',
                    VIDEO: '',
                    PDF: '',
                    TITULO: '',
                    TEXTO: $('#new-poll-question').val(),
                    OPCOES: arrOpcao
                }, function(ret) {
                    console.log(ret);
                    _BLOCK_AJAX = false;
                    $('#nova-enquete-form .submit').removeClass('submit--sending');
                    if (ret.retorno == 1) {
                        resetForms();
                        CARREGAR_TIMELINE();
                    }
                    if (ret.retorno == 2) {
                        toggleAlert('Dados Inválidos.');
                    }
                    if (ret.retorno == 3) {
                        toggleAlert('Usuário sem permissão.');
                    }
                });
                
                return false;
            }
        });

        $(document)
        .on('input', ':input', function(e){
            if ($(this).val() && $(this).val().length) {
            $(this).attr('filled', true);
            } else {
            $(this).removeAttr('filled');
            }
        })
        .on('click', '.screen-back', function(e){
            resetForms();
            sectionClose();
            // $(this).closest('.screen').hide();
            CARREGAR_TIMELINE();
            
        })
        .on('click', '.new-nav a, .screen-nav', function(e){
            e.preventDefault();
            $(this).closest('.screen').hide();
            $($(this).attr('href')).show();
        })
        .on('click', '.post-add', function(e){
            e.preventDefault();
            timeline_open = false;
            $(this).closest('.screen').hide();
            // $('#nova-postagem, #nova-enquete').show();
            resetForms();
            sectionOpen({nome:'nova-postagem'});
            // $('#nova-enquete').hide();
        })
        .on('click', '.link-nova-postagem', function(e){
            e.preventDefault();
            // resetForms();
            section_history = [{tipo:'section', section:{nome:'timeline'}}];
            sectionOpen({nome:'nova-postagem'});
        })
        .on('click', '.link-nova-enquete', function(e){
            e.preventDefault();
            // resetForms();
            section_history = [{tipo:'section', section:{nome:'timeline'}}];
            sectionOpen({nome:'nova-enquete'});
        })
        .on('input', 'textarea[maxlength]', function(e){
            textareaCounter(this);
        })
        .on('change','input[type=file]',function() {
            if (this.files.length > 0) {
                $(this).siblings('.upload').text(this.files[0].name);
            } else {
                $(this).siblings('.upload').text('Upload');
            }
        })
        .on('click', '.field-multiple .field-add', function(e){
            var $multiple = $(this).closest('.field-multiple');
            var $fields = $('.field', $multiple);
            var $lastField = $fields.eq($fields.length - 1);
            var isValid = $('input', $lastField).valid();
    
            if (isValid) {
                var $clone = $lastField.clone();
                var $cloneInput = $('input', $clone);
                var $cloneLabel = $('.label', $clone);
                
                $cloneInput.val('');
                $cloneInput.removeAttr('filled');
                $cloneInput.attr('name', 'options[' + $fields.length + ']');
                $cloneInput.attr('id','new-poll-option-' + $fields.length);
    
                $cloneLabel.attr('for','new-poll-option-' + $fields.length); 
                $cloneLabel.text('Opção ' + ($fields.length + 1));
    
                $(this).before($clone);
            }
    
        })
        .on('click', '.field-multiple .field-remove', function(e){
            $(this).closest('.field').remove();
        })
        .on('click', '.post-more a', function(e){
            $(this).hide().closest('.post').find('.post-text').css('max-height', 'none');
            return false;
        })
        .on('click', '.post-play', function(e){
            var $post = $(this).closest('.post');
            var $video = $('video', $post);
    
            $video.attr('controls', true);
            $video[0].play();
            $(this).hide();
        });
        
        $('textarea[maxlength]').each(function(index, element){
            textareaCounter(element);
        });
    
        $('input[name="date"]').datepicker({
            language: 'pt-BR',
            autoHide: true,
            startDate: new Date()
        });
    
        function resetForms() {
            //Post
            $('form','#nova-postagem').get(0).reset();
            $('input[type=file]','#nova-postagem').trigger('change');
            $(':input','#nova-postagem').trigger('input');
            $('form','#nova-postagem').data('validator').resetForm();
            //Enquete
            $('form','#nova-enquete').get(0).reset();
            $('.field-multiple .field','#nova-enquete').not(':eq(0)').remove();
            $('input[name="date"]','#nova-enquete').datepicker('reset');
            $(':input','#nova-enquete').trigger('input');
            $('form','#nova-enquete').data('validator').resetForm();
        }
    
        function textareaCounter(element){
            var maxLen = $(element).attr('maxlength');
            var curLen = $(element).val().length;
            var $chars = $(element).siblings('.chars');
    
            $chars.text(curLen + '/' + maxLen)
        }
    
        var montaNotificacao = function() {
            if (window.PushNotification != undefined) {
                var push = PushNotification.init({
                    "android": {
                        "icon": "notification",
                        "iconColor": "#79AC58"
                    },
                    "browser": {},
                    "ios": {
                        "sound": true,
                        "vibration": true,
                        "badge": true
                    },
                    "windows": {}
                });
                push.on('registration', function(data) {
                    console.log('push registration');
                    console.log('data.registrationId = ' + data.registrationId);
                    console.log('device.platform = ' + device.platform);
                    var platform = '';
                    try {
                        platform = device.platform;
                    } catch (e) {
                    }
                    NOTIFICATION_REGISTER(data.registrationId, platform);
                });
                push.on('error', function(e) {
                    console.log("push errorr = " + e.message);
                });
                push.on('notification', function(data) {
                    console.log('notification event');
                    CHECA_NOTIFICACAO();
                });
            }
        };
        montaNotificacao();
    
        document.addEventListener("resume", function() {
            CHECA_NOTIFICACAO();
        });
    
        if ((localStorage.getItem('WS_TOKEN') != null) && (WS_TOKEN == '')) {
            WS.RecuperaSessao();
            VALIDA_SESSAO();
        } else {
            $('#bx-splash').fadeOut();
            // $('#welcome').show();
            sectionChange({nome:'welcome'});
        }
        $('#main').show();
    
        try {
            StatusBar.backgroundColorByHexString('#75AE50');
            StatusBar.overlaysWebView(false);
            StatusBar.styleBlackOpaque();
        } catch (e) {
        }

        Setup_WebPush();

    });
};

var scriptLoadedCount = 0;
var arrScripts = [
    'js/modernizr-2.8.3.min.js',
    'js/plugins.js',
    'js/ws.js',
];

var processjQueryLoad = function() {
    var script = document.createElement('script');
    script.onload = function () {
        scriptLoadedCount++;
        if (scriptLoadedCount == arrScripts.length) {
            processPageLoad();
        } else {
            processjQueryLoad();
        }
    };
    script.src = arrScripts[scriptLoadedCount] + '?v=' + Math.random();
    document.head.appendChild(script);
};

if (window.jQuery) {
    processjQueryLoad();
} else {
    var script = document.createElement('script');
    script.onload = function () {
        processjQueryLoad();
    };
    script.src = 'js/jquery-1.12.0.min.js';
    document.head.appendChild(script);
}

//NOTIFICATION
var NOTIFICATION_REGISTER = function(registrationId, platform) {
	console.log('NOTIFICATION_REGISTER');
	WS_NOTIFICATION_FCM = registrationId;
	WS_PLATAFORM = ((platform.toUpperCase().indexOf('IOS') != -1) ? 2 : 3); //1 - Desktop, 2 - iOS, 3 - Android
	SEND_NOTIFICATION_FCM();
};
var LAST_NOTIFICATION_FCM = '';
var SEND_NOTIFICATION_FCM = function() {
	var data = {fcm:WS_NOTIFICATION_FCM, plataform:WS_PLATAFORM};
	var tmp_data = JSON.stringify({WS_TOKEN:WS_TOKEN, DATA:data});
	if ((WS_TOKEN != '') && (WS_NOTIFICATION_FCM != '') && (LAST_NOTIFICATION_FCM != tmp_data)) {
		LAST_NOTIFICATION_FCM = tmp_data;
		WS.InserirRequisicao('GRAVA_PUSH', data, function(ret) {
			if (ret.retorno != 1) {
				console.log('Erro ao gravar o NOTIFICATION token.');
			}
		});
	}
};

var CHECA_NOTIFICACAO = function() {
    if (timeline_open) {
        CARREGAR_TIMELINE();
    }
};

var Setup_WebPush = function() {
    if (location.href.toLowerCase().indexOf('file://') != 0) {

        const messaging = firebase.messaging();

        var sendTokenToServer = function(token) {
            WS_NOTIFICATION_FCM = token;
            WS_PLATAFORM = 1;
            SEND_NOTIFICATION_FCM();
        };
        
        messaging.usePublicVapidKey("BJhO0fA7hg6o7T3O72ZEGU1V1Ni6ONCnp6gKlTDqeVJjig9e7VkdhwG-DT5DcZgsdMOcfZW9yp9nw4WffKFij2c");

        messaging.getToken().then(function(currentToken) {
            if (currentToken) {
                sendTokenToServer(currentToken);
            } else {
                messaging.requestPermission().then(function() {
                    messaging.getToken().then(function(currentToken) {
                        if (currentToken) {
                            sendTokenToServer(currentToken);
                        }
                    });
                }).catch(function(err) {
                    console.log('Unable to get permission to notify.', err);
                });
            }
        }).catch(function(err) {
            console.log('An error occurred while retrieving token. ', err);
        });

        messaging.onTokenRefresh(function() {
            messaging.getToken().then(function(refreshedToken) {
                sendTokenToServer(refreshedToken);
            }).catch(function(err) {
                console.log('Unable to retrieve refreshed token ', err);
            });
        });

        messaging.onMessage(function(payload) {
            CHECA_NOTIFICACAO();
        });
    }
};