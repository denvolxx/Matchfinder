import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);
  busyService.busy();

  return next(req).pipe(
    //TODO: Remove loader when tested
    //Delay to test loader and observables (no outgoing requests to API => data stored in observables => loader is not activated)
    delay(1000), 

    finalize(() => {
      busyService.idle();
    })
  );
};
