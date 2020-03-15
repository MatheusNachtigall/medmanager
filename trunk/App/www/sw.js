
var CACHE_NAME = 'static-v1';

self.addEventListener('install', function (event) {
  event.waitUntil(
    caches.open(CACHE_NAME).then(function (cache) {
      return cache.addAll([
        'index.html',
        'css/materialize.min.css',
        'css/main.css',
        'css/dropzone.css',
        'css/fullCalendar.min.css',
        'js/ws.js',
        'js/plugins/jquery.min.js',
        'js/plugins/jquery-ui.min.js',
        'js/plugins/jquery.ui.touch-punch.min.js',
        'js/plugins/materialize.min.js',
        'js/plugins/jquery.mask.min.js',
        'js/plugins/chart.js',
        'js/plugins/fullCalendar.min.js',
        'js/plugins/moment-with-locales.min.js',
        'js/plugins/fullcalendar_moment.js',
        'js/charts_Data.js',
        'js/requisicoes.js',
        'js/app.js',
        'js/plugins/dropzone.js'
      ]);
    })
  )
});

self.addEventListener('activate', function activator(event) {
  event.waitUntil(
    caches.keys().then(function (keys) {
      return Promise.all(keys
        .filter(function (key) {
          return key.indexOf(CACHE_NAME) !== 0;
        })
        .map(function (key) {
          return caches.delete(key);
        })
      );
    })
  );
});

self.addEventListener('fetch', function (event) {
  event.respondWith(
    caches.match(event.request).then(function (cachedResponse) {
      return cachedResponse || fetch(event.request);
    })
  );
});