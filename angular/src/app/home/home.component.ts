import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { HomeService } from './home.service';
import { ToastrService } from 'ngx-toastr';
import { FirebaseService } from './firebase.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  books: any[] = [];
  newBook = {
    id: '',
    name: '',
    authorName: '',
    price: 0,
    publishDate: ''
  };
  isEditMode = false;
  showForm = false;
  showPopup = false; // Hiển thị popup

  private subscription: Subscription = new Subscription();

  constructor(private homeService: HomeService, private toastr: ToastrService, private firebaseService: FirebaseService) {}

  ngOnInit(): void {
    this.loadBooks();
    this.initializeFirebase();
  }

  loadBooks() {
    this.subscription.add(
      this.homeService.getBooks().subscribe(
        (data) => {
          this.books = data;
        },
        (error) => this.handleHttpError(error)
      )
    );
  }

  initializeFirebase() {
    if ('serviceWorker' in navigator) {
      navigator.serviceWorker.register('/firebase-messaging-sw.js')
        .then((registration) => {
          console.log('Service Worker registered with scope:', registration.scope);
          return this.firebaseService.getToken();
        })
        .then(token => {
          console.log('FCM Registration Token:', token);
        })
        .catch(err => {
          console.error('Error retrieving FCM token:', err);
          this.toastr.error(err.message, 'Error', {
            timeOut: 3000,
            positionClass: 'toast-bottom-right',
            progressBar: true
          });
        });

      this.firebaseService.listenForMessages();
    }
  }
  
  onSubmit() {
    if (this.isEditMode) {
      this.updateBook();
    } else {
      this.createBook();
    }
  }

  createBook() {
    console.log('Form submitted with:', this.newBook);
    this.subscription.add(
      this.homeService.createBook(this.newBook).subscribe(
        (response) => {
          console.log('Book created successfully:', response);
          this.books.push(response);
          this.resetForm();
          this.loadBooks();
          this.toggleBookForm();
        },
        (error) => this.handleHttpError(error)
      )
    );
  }

  updateBook() {
    const updatedBook = {
      name: this.newBook.name,
      authorName: this.newBook.authorName,
      price: this.newBook.price,
      publishDate: new Date(this.newBook.publishDate).toISOString()
    };
    this.subscription.add(
      this.homeService.updateBook(this.newBook.id, updatedBook).subscribe(
        (response) => {
          console.log('Book updated successfully:', response);
          this.loadBooks();
          this.resetForm();
          this.closePopup(); // Đóng popup sau khi cập nhật
        },
        (error) => this.handleHttpError(error)
      )
    );
  }

  deleteBook(bookId: string) {
    this.subscription.add(
      this.homeService.deleteBook(bookId).subscribe(
        () => {
          this.books = this.books.filter(b => b.id !== bookId);
        },
        (error) => this.handleHttpError(error)
      )
    );
  }

  editBook(book) {
    this.newBook = { 
      id: book.id,
      name: book.name, 
      authorName: book.authorName, 
      price: book.price, 
      publishDate: new Date(book.publishDate).toISOString().substring(0, 10)
    };
    this.isEditMode = true;
    this.showPopup = true; // Hiển thị popup khi chỉnh sửa
  }

  closePopup() {
    this.showPopup = false; // Đóng popup
    this.resetForm(); // Reset form khi đóng popup
  }

  toggleBookForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.resetForm();
    }
  }

  resetForm() {
    this.newBook = {
      id: '',
      name: '',
      authorName: '',
      price: 0,
      publishDate: ''
    };
    this.isEditMode = false;
    this.showPopup = false; // Ẩn popup khi reset form
  }

  handleHttpError(error: any) {
    if (error.status === 401) {
      this.toastr.error('Please log in.', 'Error', {
        timeOut: 3000,
        positionClass: 'toast-bottom-right',
        
        progressBar: true
      });
    } else if (error.status === 403) {
      this.toastr.error('you do not have permission.', 'Error', {
        timeOut: 3000,
        positionClass: 'toast-bottom-right',
        
        progressBar: true
      });
    } else {
      this.toastr.error('An unexpected error occurred.', 'Error', {
        timeOut: 3000,
        positionClass: 'toast-bottom-right',
        
        progressBar: true
      });
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe(); // Hủy đăng ký subscription để tránh rò rỉ bộ nhớ
  }
}
