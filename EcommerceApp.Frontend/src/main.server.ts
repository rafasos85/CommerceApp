import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { config } from './app/app.config.server';

/*const bootstrap = () => bootstrapApplication(App, config);

export default bootstrap;*/

export default function(context: any) {
  return bootstrapApplication(App, config, context);
}