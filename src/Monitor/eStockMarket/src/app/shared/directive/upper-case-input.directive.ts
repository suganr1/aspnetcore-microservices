import { Directive, HostListener } from '@angular/core';

@Directive({
    selector: '[appUpperCase]'
})

export class UpperCaseInputDirective {
    @HostListener('input', ['$event']) onInput(event: any) {
        //console.log("UpperCaseInputDirective::event", event);
        event.target.value = event.target.value.toUpperCase();
        return true;
    }
}
