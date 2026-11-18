import { JsonPipe } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  imports: [RouterOutlet,JsonPipe],
  selector: 'app-root',
  styles: [],
  template: `
    <h1>Hello, {{ title() }}</h1>
    <p contenteditable="true">You can click me and edit me!</p>

    <pre>
    {{ vendorResource.value() | json }}
    </pre>
    <router-outlet />
  `,
})
export class App {
  protected readonly title = signal('frontend');
  private readonly vendorResource = httpResource(() => '/api/vendors');
}
