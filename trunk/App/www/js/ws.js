var WS_HTTP_URL = '';
var WS_SOCKET_URL = '';
var WS_SOCKET = (getParameterByName('socket') == '1');
var WS_TIMEOUT = -1;
var WS_INTERVAL = 500;
var WS_USUARIO_ID = 0;
var WS_TOKEN = '';
var WS_NOTIFICATION_FCM = '';
var WS_NOTIFICATION_PWA = null;
var WS_PLATAFORM = '';
var WS_DELAY = (getParameterByName('delay') == null) ? 500 : getParameterByName('delay');
var PERSONIFICANDO = false;
var _BLOCK_AJAX = false;

var BACKEND = 'http://localhost:55727/';

if (location.href.indexOf('localhost') != -1) {
	WS_SOCKET_URL = 'ws://localhost:55727/WSH.ashx';
	WS_HTTP_URL = 'http://localhost:55727/WS.aspx';
}

var WS = {
	conn: null,
	arrRequisicaoPendente: [],
	arrRequisicaoEnviada: [],
	Conectado:false,
	AtualizaSessao: function(usuario_id, token) {
		WS_USUARIO_ID = usuario_id;
		WS_TOKEN = token;
		if (!PERSONIFICANDO) {
			localStorage.setItem('WS_USUARIO_ID', WS_USUARIO_ID);
			localStorage.setItem('WS_TOKEN', WS_TOKEN);
		}
	},
	RecuperaSessao: function() {
		WS_USUARIO_ID = (localStorage.getItem('WS_USUARIO_ID') != null) ? localStorage.getItem('WS_USUARIO_ID') : WS_USUARIO_ID;
		WS_TOKEN = (localStorage.getItem('WS_TOKEN') != null) ? localStorage.getItem('WS_TOKEN') : WS_TOKEN;
	},
	Logout:function() {
		localStorage.removeItem('WS_USUARIO_ID');
		localStorage.removeItem('WS_TOKEN');
		location.reload(true);
	},
	InserirRequisicao: function(action, data, cb) {
		var req = {
			action: action,
			data: JSON.stringify(data),
			usuario_id: WS_USUARIO_ID,
			token: WS_TOKEN,
			hash: WS.GerarRequisicaoHash(),
			cb: cb,
			time: new Date().getTime(),
			reenvio:false
		};
		WS.arrRequisicaoPendente.push(req);
		WS.IniciarConexao();
	},
	ProcessarRequisicao: function() {
		while (WS.arrRequisicaoPendente.length > 0) {
			try {
				if (WS_SOCKET) {
					WS.conn.send(JSON.stringify(WS.GetReq(WS.arrRequisicaoPendente[0], false, false)));
				} else {
					$.ajax({
						type: "POST",
						url: WS_HTTP_URL,
						cache: false,
						data: {
							data: JSON.stringify(WS.GetReq(WS.arrRequisicaoPendente[0], false, false))
						},
						context: WS.arrRequisicaoPendente[0],
						success: function(data, textStatus, XMLHttpRequest) {
							WS.ProcessaRetorno(data);
						},
						complete: function(XMLHttpRequest, textStatus) {
							for (var i = 0; i < WS.arrRequisicaoEnviada.length; i++) {
								if (WS.arrRequisicaoEnviada[i].hash == this.hash) {
									WS.ReenviaRequisicao(WS.arrRequisicaoEnviada[i]);
									WS.arrRequisicaoEnviada.splice(i, 1);
									if (WS_TIMEOUT == -1) WS_TIMEOUT = setTimeout(WS.IniciarConexao, WS_INTERVAL);
									break;
								}
							}
						}
					});
				}
				WS.arrRequisicaoEnviada.push(WS.arrRequisicaoPendente[0]);
				WS.arrRequisicaoPendente.splice(0, 1);
			} catch (error) {
				console.log(error);
				break;
			}
		}
	},
	ProcessaRetorno: function(ret) {
		var data = JSON.parse(ret);
		if (data.sucesso) {
			for (var i = 0; i < WS.arrRequisicaoEnviada.length; i++) {
				if (WS.arrRequisicaoEnviada[i].hash == data.hash) {
					if (data.data != 'TRANSACAO_PENDENTE') {
						try {
							var reqTime = new Date().getTime() - WS.arrRequisicaoEnviada[i].time;
							var toDelay = WS_DELAY - reqTime;
							toDelay = (toDelay < 0) ? 0 : toDelay;
							window.setTimeout(WS.arrRequisicaoEnviada[i].cb, toDelay, ((data.data != "") ? JSON.parse(data.data) : ""));
						} catch (error) {
							console.log(error);
						}
					} else {
						WS.ReenviaRequisicao(WS.arrRequisicaoEnviada[i]);
						if (WS_TIMEOUT == -1) WS_TIMEOUT = setTimeout(WS.IniciarConexao, WS_INTERVAL);
					}
					WS.arrRequisicaoEnviada.splice(i, 1);
					break;
				}
			}
		} else {
			WS.arrRequisicaoPendente = [];
			WS.arrRequisicaoEnviada = [];
			if (data.motivo == 'SESSAO_INVALIDA') {
				WS.Logout();
			}
			if (data.motivo == 'SISTEMA_INDISPONIVEL') {
				_BLOCK_AJAX = false;
				$('.submit--sending').removeClass('submit--sending');
				toggleAlert('Sistema indisponível');
			}
		}
	},
	GetReq: function(req, reenvio, interno) {
		var tmp = {
			action: req.action,
			data: req.data,
			usuario_id: req.usuario_id,
			token: req.token,
			hash: req.hash,
			reenvio: (reenvio) ? true : req.reenvio
		};
		if (interno) {
			tmp.cb = req.cb;
			tmp.time = req.time;
		}
		return tmp;
	},
	ReenviaRequisicao: function(req) {
		WS.arrRequisicaoPendente.push(WS.GetReq(req, true, true));
	},
	IniciarConexao: function() {
		if (WS_SOCKET) {
			if (WS.Conectado) {
				WS.ProcessarRequisicao();
			} else {
				WS.conn = new WebSocket(WS_SOCKET_URL);
				WS.conn.onopen = function (e) {
					WS.Conectado = true;
					while (WS.arrRequisicaoEnviada.length > 0) {
						WS.ReenviaRequisicao(WS.arrRequisicaoEnviada[0]);
						WS.arrRequisicaoEnviada.splice(0, 1);
					}
					WS.ProcessarRequisicao();
				};
				WS.conn.onmessage = function (e) {
					WS.ProcessaRetorno(e.data);
				};
				WS.conn.onclose = function () {
					WS.ConexaoDown();
				};
				WS.conn.onerror = function (e) {
					WS.ConexaoDown();
				}
			}
		} else {
			WS.ProcessarRequisicao();
		}
		WS_TIMEOUT = -1;
	},
	ConexaoDown: function() {
		WS.Conectado = false;
		if ((WS.arrRequisicaoPendente.length > 0) || (WS.arrRequisicaoEnviada.length > 0)) {
			if (WS_TIMEOUT == -1) WS_TIMEOUT = setTimeout(WS.IniciarConexao, WS_INTERVAL);
		}
	},
	GerarRequisicaoHash: function() {
		return Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
	}
};
WS.IniciarConexao();

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

var IsMobile = (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent));