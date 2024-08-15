import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

if (environment.production || environment.local) {  // thêm điều kiện cho local
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/firebase-messaging-sw.js')
      .then((registration) => {
        console.log('Service Worker registered with scope:', registration.scope);
      })
      .catch((error) => {
        console.error('Service Worker registration failed:', error);
      });
  }
}

if (environment.production || environment.local) {  // thêm điều kiện cho local
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/firebase-messaging-sw2.js')
      .then((registration) => {
        console.log('Service Worker registered user 2 with scope:', registration.scope);
      })
      .catch((error) => {
        console.error('Service Worker registration user 2 failed:', error);
      });
  }
}

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch(err => console.error(err));
