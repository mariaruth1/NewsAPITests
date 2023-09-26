import { Component } from '@angular/core';
import {FormControl, FormGroup, MaxValidator, PatternValidator, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {firstValueFrom} from "rxjs";
import {CreateOrUpdateArticle} from "../Models/Models";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private httpClient: HttpClient) {
    this.get();
  }

  articles: CreateOrUpdateArticle[]=[];

  title = 'NewsFeed';

  headerInput= new FormControl('',
    [Validators.required, Validators.minLength(5), Validators.maxLength(30)]);
  bodyInput= new FormControl('',
    [Validators.required, Validators.maxLength(1000)]);
  authorInput = new FormControl('', [Validators.required, Validators.pattern("(?:Bob|Rob|Lob|Dob|bob|rob|dob|lob)")]);
  urlInput = new FormControl('', Validators.required);


  inputGroup = new FormGroup({
    headline: this.headerInput!,
    author: this.authorInput!,
    body: this.bodyInput!,
    articleImgUrl: this.urlInput!
  })

  async get(){
    const articleReq = this.httpClient.get<CreateOrUpdateArticle[]>('http://localhost:5000/api/feed');
    this.articles = await firstValueFrom<CreateOrUpdateArticle[]>(articleReq);
  }

  async send() {
    const dto = this.inputGroup.value as CreateOrUpdateArticle;
    const result = this.httpClient.post("http://localhost:5000/api/articles", dto);
    const value = await firstValueFrom(result);
    console.log(value);
  }

  async delete(){
    this.articles.pop()
  }
}
