import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiUrl = 'https://localhost:44375/api/Book';

  constructor(private http: HttpClient) { }

  // Phương thức để gọi API lấy danh sách sách từ server
  getBooks(): Observable<any[]> {
    const url = `${this.apiUrl}/books`;
    return this.http.get<any[]>(url);
  }

  // Phương thức để gọi API tạo mới sách
  createBook(token: string, deviceId:string, newBook: any): Observable<any> {
    const url = `${this.apiUrl}/${token}/${deviceId}`;
    return this.http.post<any>(url, newBook);
  }

  // Phương thức để gọi API cập nhật thông tin sách
  updateBook(token: string, deviceId: string, bookId: string, updatedBook: any): Observable<any> {
    const url = `${this.apiUrl}/${token}/${deviceId}/${bookId}`;
    return this.http.put<any>(url, updatedBook);
  }

  // Phương thức để gọi API xoá sách
  deleteBook(token: string, deviceId:string, bookId: string): Observable<any> {
    const url = `${this.apiUrl}/${token}/${deviceId}/${bookId}`;
    return this.http.delete<any>(url);
  }

  importBookExcel(file: File): Observable<any> {
    const url = `${this.apiUrl}/ImportBookFromExcel`;  
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post<any>(url, formData);
  }


}
