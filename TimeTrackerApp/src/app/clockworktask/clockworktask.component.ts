import {Component} from '@angular/core';
import {ClockworktaskService} from "./clockworktask.service";

@Component({
  selector: 'app-clockworktask',
  templateUrl: './clockworktask.component.html',
  styleUrls: ['./clockworktask.component.css'],
  providers: [ClockworktaskService]
})

export class ClockworktaskComponent {
  constructor(private clockworktaskService: ClockworktaskService) {}

  getClockworktasks() {
    return this.clockworktaskService.clockworktaskList.slice();
  }

}
