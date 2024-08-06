import { Injectable } from '@angular/core';
import { initializeApp } from 'firebase/app';
import { getMessaging, getToken, onMessage } from 'firebase/messaging';

@Injectable({
  providedIn: 'root'
})
export class FirebaseService {
  private messaging;

  constructor() {
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

    // Initialize Firebase
    initializeApp(firebaseConfig);
    this.messaging = getMessaging();
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

  listenForMessages() {
    onMessage(this.messaging, (payload) => {
      console.log('Message received: ', payload);
    });
  }
}
