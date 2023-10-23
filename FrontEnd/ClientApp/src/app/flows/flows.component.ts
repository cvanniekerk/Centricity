import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-flows',
  templateUrl: './flows.component.html'
})
export class FlowsComponent {

  public flows: Flow[] = [];
  public jobHistory: any;

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string, private router: Router) {
    http.get<Flow[]>(apiUrl + 'flows').subscribe(result => {
      this.flows = result;
    }, error => console.error(error));
    http.get<any>(apiUrl + 'job/history').subscribe(result => {
      this.jobHistory = result;
    }, error => console.error(error));
  }
}

interface Flow {
  id: number,
  name: string
}
