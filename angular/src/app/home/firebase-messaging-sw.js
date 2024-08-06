// firebase-messaging-sw.js

importScripts('https://www.gstatic.com/firebasejs/9.1.3/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/9.1.3/firebase-messaging.js');

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

// Handle background messages
messaging.onBackgroundMessage((payload) => {
  console.log('Received background message ', payload);
  const notificationTitle = 'Background Message Title';
  const notificationOptions = {
    body: 'Background message body.',
    icon: '/firebase-logo.png'
  };

  self.registration.showNotification(notificationTitle, notificationOptions);
});
