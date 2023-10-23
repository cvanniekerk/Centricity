import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent {

  public job: any;
  public jobStepIndex: number = 0;

  constructor(
    private http: HttpClient,
    @Inject('API_URL') private apiUrl: string,
    private router: Router,
    private route: ActivatedRoute) {
    
    var flowId = this.route.snapshot.paramMap.get('flowId');

    http.get(apiUrl + `job/createJobForFlow/${flowId}`).subscribe(result => {
      this.job = result;
    }, error => console.error(error));
  }

  public updateEvidence(evidence: any) {
    // update evidence in job
    this.job.jobSteps[this.jobStepIndex].evidence.forEach((e: { id: any; value: any; }) => {
      if (e.id == evidence.id) {
        e.value = evidence.value.toString();
      }
    });    
  }

  public doTransition(transition: any) {
    console.log(this.job.jobSteps[this.jobStepIndex].evidence);

    // update all evidence for current step
    let myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    this.http.post(this.apiUrl + 'evidence', this.job.jobSteps[this.jobStepIndex].evidence, { headers : myHeaders }).subscribe(result => {

      if (!transition.isEnd) {
        this.jobStepIndex++;
      }
      else {
        this.http.get(this.apiUrl + `job/complete/${this.job.id}`).subscribe(restul => {
          this.router.navigate(['/flows']);
        }, error => console.log(error));        
      }      
    }, error => console.log(error));    
  }
}



