import { KeycloakService } from 'keycloak-angular';
import { from, switchMap } from 'rxjs';
import { IAMService } from 'src/app/Services/auth/iam.service';
import { TenantConfig } from './iam-tenant';

export function initializeKeycloak(keycloak: KeycloakService, _iamService: IAMService) {

    return () => from(_iamService.getTenantConfiguration())
                    .pipe(switchMap<any, any>(response =>{
                                     
                        const tenantConfig: TenantConfig = response.result;
                        return keycloak.init(
                            {
                            config:   {
                                url: tenantConfig.iamUrl,
                                realm: tenantConfig.tenantName,
                                clientId: tenantConfig.clientId,
                            },initOptions: {
                                onLoad: 'login-required',                                
                                checkLoginIframe: false, 
                                pkceMethod: 'S256',
                                //silentCheckSsoRedirectUri: tenantConfig.initOptions.redirectUri,
                            },
                            enableBearerInterceptor: true,
                            bearerPrefix: 'Bearer',
                            bearerExcludedUrls: ['/assets']
                            });

                    }));
    
                }
