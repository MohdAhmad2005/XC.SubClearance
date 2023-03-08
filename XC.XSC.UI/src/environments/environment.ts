export const environment = {
  production: true,  
  encryption: { AesKey: '80808080808080808080808080808080', AesIV: '8080808080808080' },
  verificationCodeLength: 6,
  errorMessage: "Some error occured. Please try again.",
  sessionTimeout: 120, //in seconds
  DefaultPageSize: 10,  
  AuthenticationMode: '',// 'System',
  dateFormat: 'DD/MM/YYYY',
  domain: 'https://xsc-dev.itssxc.com/',
  xscApiUrl: 'https://xsc-api-dev.itssxc.com/api/xsc',
  uamApiUrl: 'https://ccmp-uam-api-dev.itssxc.com',
  ruleEngineApiUrl: 'https://ccmp-ruleengine-api-dev.itssxc.com/'
};
