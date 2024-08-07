importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-messaging-compat.js');

const firebaseConfig = {
  apiKey: "AIzaSyDqykB0pj4ZwhkaIgsjYfVOSN0ukDqt5Nk",
  authDomain: "testsendnoti-f5a2c.firebaseapp.com",
  projectId: "testsendnoti-f5a2c",
  storageBucket: "testsendnoti-f5a2c.appspot.com",
  messagingSenderId: "1076466200757",
  appId: "1:1076466200757:web:c6f7b36348e74e875e0efc",
  measurementId: "G-G4QF7FHRR9"
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();

messaging.onBackgroundMessage((payload) => {
  console.log('[firebase-messaging-sw.js] Received background message ', payload);

  // Customize notification here
  const notificationTitle = payload.notification.title;
  const notificationOptions = {
    title: payload.notification.title,
    body: payload.notification.body,
  };

  self.registration.showNotification(notificationTitle, notificationOptions);
});
