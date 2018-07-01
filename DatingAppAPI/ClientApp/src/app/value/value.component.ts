import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-value',
    templateUrl: './value.component.html',
})
/** value component*/
export class ValueComponent {
  values: Value[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Value[]>(baseUrl + 'api/Values/GetValues').subscribe(result => { this.values = result }, error => console.error(error));
  }

}


interface Value {
  id: number,
  name: string
}

