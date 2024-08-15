import { Injectable } from '@angular/core';
import { getMessaging, getToken, onMessage } from 'firebase/messaging';
import { ToastrService } from 'ngx-toastr';
import { initializeApp, getApps, getApp } from 'firebase/app';


@Injectable({
  providedIn: 'root'
})
export class FirebaseService {
  private messaging;
  private messaging2;
  constructor(private toastr: ToastrService) {
    const firebaseConfig = {
      apiKey: "AIzaSyDqykB0pj4ZwhkaIgsjYfVOSN0ukDqt5Nk",
      authDomain: "testsendnoti-f5a2c.firebaseapp.com",
      projectId: "testsendnoti-f5a2c",
      storageBucket: "testsendnoti-f5a2c.appspot.com",
      messagingSenderId: "1076466200757",
      appId: "1:1076466200757:web:c6f7b36348e74e875e0efc",
      measurementId: "G-G4QF7FHRR9",
      vapidKey: 'BLY_x2PknOsPaEanKox1kA8KTRBLPIKYIsfS6ao8z4842NhaSHhZa7t4q75xZWTDp_7gUHhfOJdo9OpGbKISeow'
    };

    const firebaseConfig2 = {
      apiKey: "AIzaSyDqykB0pj4ZwhkaIgsjYfVOSN0ukDqt5Nk",
      authDomain: "testsendnoti-f5a2c.firebaseapp.com",
      databaseURL: "https://testsendnoti-f5a2c-default-rtdb.asia-southeast1.firebasedatabase.app",
      projectId: "testsendnoti-f5a2c",
      storageBucket: "testsendnoti-f5a2c.appspot.com",
      messagingSenderId: "1076466200757",
      appId: "1:1076466200757:web:ff8d5d70e03a7c6a5e0efc",
      measurementId: "G-S5VG94M8BM",
      vapidKey: 'BLY_x2PknOsPaEanKox1kA8KTRBLPIKYIsfS6ao8z4842NhaSHhZa7t4q75xZWTDp_7gUHhfOJdo9OpGbKISeow'
    };

    // Initialize Firebase
    const app1 = !getApps().length ? initializeApp(firebaseConfig) : getApp();
    const app2 = initializeApp(firebaseConfig2, 'app2');
    this.messaging = getMessaging();
    this.messaging2 = getMessaging(app2);
  }

  async getToken(): Promise<string> {
    try {
      const permission = Notification.permission;

      if (permission === 'default') {
        const granted = await Notification.requestPermission();
        if (granted !== 'granted') {
          throw new Error('Notification permission not granted.');
        }
      } else if (permission === 'denied') {
        throw new Error('Notification permission was blocked.');
      }

      // Use the VAPID key directly
      const currentToken = await getToken(this.messaging, { vapidKey: 'BLY_x2PknOsPaEanKox1kA8KTRBLPIKYIsfS6ao8z4842NhaSHhZa7t4q75xZWTDp_7gUHhfOJdo9OpGbKISeow' });
      if (!currentToken) {
        console.log('No registration token available.');
      }
      return currentToken || '';
    } catch (err) {
      console.error('Error getting registration token:', err);
      throw err;
    }
  }

  async getToken2(): Promise<string> {
    try {
      const permission = Notification.permission;

      if (permission === 'default') {
        const granted = await Notification.requestPermission();
        if (granted !== 'granted') {
          throw new Error('Notification permission not granted.');
        }
      } else if (permission === 'denied') {
        throw new Error('Notification permission was blocked.');
      }

      // Use the VAPID key directly
      const currentToken = await getToken(this.messaging2, { vapidKey: 'BLY_x2PknOsPaEanKox1kA8KTRBLPIKYIsfS6ao8z4842NhaSHhZa7t4q75xZWTDp_7gUHhfOJdo9OpGbKISeow' });
      if (!currentToken) {
        console.log('No registration token available.');
      }
      return currentToken || '';
    } catch (err) {
      console.error('Error getting registration token:', err);
      throw err;
    }
  }

    listenForMessages() {
      onMessage(this.messaging, (payload) => {
        console.log('Message received: ', payload);
        this.toastr.info(payload.notification.body, payload.notification.title, {
          timeOut: 5000,
          positionClass: 'toast-top-right'
        });
      });
    }
    
}
