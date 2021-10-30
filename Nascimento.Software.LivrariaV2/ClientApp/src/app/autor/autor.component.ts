import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.css']
})
export class AutorComponent implements OnInit {
public autores: any = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAutores();
  }


  public getAutores(): void{
    this.http.get('https://localhost:44342/api/Autor/Get').subscribe(
      response => this.autores = response,
      error => console.error(error))
  }

}
