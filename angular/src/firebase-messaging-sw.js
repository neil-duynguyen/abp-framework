importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-messaging-compat.js');


firebase.initializeApp({
  apiKey: "AIzaSyDqykB0pj4ZwhkaIgsjYfVOSN0ukDqt5Nk",
  authDomain: "testsendnoti-f5a2c.firebaseapp.com",
  projectId: "testsendnoti-f5a2c",
  storageBucket: "testsendnoti-f5a2c.appspot.com",
  messagingSenderId: "1076466200757",
  appId: "1:1076466200757:web:c6f7b36348e74e875e0efc",
  measurementId: "G-G4QF7FHRR9"
});

const messaging = firebase.messaging();
