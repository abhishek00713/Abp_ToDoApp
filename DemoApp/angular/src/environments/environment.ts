import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Team Next ToDo ',
    logoUrl: 'http://teamnext.com.np/Content/img/TN_Logo.png',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44359',
    redirectUri: baseUrl,
    clientId: 'DemoApp_App',
    responseType: 'code',
    scope: 'offline_access openid profile role email phone DemoApp',
  },
  apis: {
    default: {
      url: 'https://localhost:44335',
      rootNamespace: 'DemoApp',
    },
  },
} as Environment;
