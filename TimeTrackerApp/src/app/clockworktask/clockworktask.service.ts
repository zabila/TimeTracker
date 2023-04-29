import {Injectable} from '@angular/core';
import {Clockworktask} from "./clockworktask.model";

@Injectable({
  providedIn: 'root'
})
export class ClockworktaskService {

  clockworktaskList: Clockworktask[] = [
    {
      clockworkTaskId: '1',
      clockworkTaskKey: '1',
      startedDateTime: new Date(),
      timeSpentSeconds: 1,
      accountId: '1'
    },
    {
      clockworkTaskId: '2',
      clockworkTaskKey: '2',
      startedDateTime: new Date(),
      timeSpentSeconds: 2,
      accountId: '2'
    },
    {
      clockworkTaskId: '3',
      clockworkTaskKey: '3',
      startedDateTime: new Date(),
      timeSpentSeconds: 3,
      accountId: '3'
    },
  ];


}
