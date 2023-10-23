import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job-history',
  templateUrl: './job-history.component.html',
  styleUrls: ['./job-history.component.css']
})
export class JobHistoryComponent {

  public job: any;

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string, private router: Router, private route: ActivatedRoute) {

    var jobId = this.route.snapshot.paramMap.get('jobId');

    http.get<any>(apiUrl + `job/history/${jobId}`).subscribe(result => {
      this.job = result;
    }, error => console.error(error));    
  }
}


