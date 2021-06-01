import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'DemoApp',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44359',
    redirectUri: baseUrl,
    clientId: 'DemoApp_App',
    responseType: 'code',
    scope: 'offline_access DemoApp',
  },
  apis: {
    default: {
      url: 'https://localhost:44335',
      rootNamespace: 'DemoApp',
    },
  },
} as Environment;
