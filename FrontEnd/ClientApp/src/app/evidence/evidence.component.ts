import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-evidence',
  templateUrl: './evidence.component.html',
  styleUrls: ['./evidence.component.css']
})
export class EvidenceComponent {

  @Input('evidence') evidence: any;  
  @Output() updated = new EventEmitter();

  updateText(event: any) {
    this.evidence.value = event.target.value;
    this.updated.emit(this.evidence);
  }

  updateCheckbox(event: any) {
    this.evidence.value = event.target.checked;    
    this.updated.emit(this.evidence);
  }
}



