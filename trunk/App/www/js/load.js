var scriptLoadedCount = 0;
var arrScripts = [
    "js/ws.js",
    "js/plugins/jquery.min.js",
    'js/jquery-ui.min.js',
    "js/plugins/materialize.min.js",
    "js/plugins/jquery.mask.min.js",
    "js/plugins/dropzone.js",
    "js/plugins/chart.js",
    "js/charts_Data.js",
    "js/requisicoes.js",
    "js/app.js"
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



