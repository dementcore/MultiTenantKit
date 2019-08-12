# MultiTenantKit Domain Resolution Sample
This sample shows tenant resolution from route subdomain.
# Configuration
You may need to add following entries to your host file to make this sample work.
- 127.0.0.1 domain.com
- 127.0.0.1 tenant1.domain.com
- 127.0.0.1 tenant2.domain.com

# Using the sample
Run the sample.
- Navigate to http://tenant1.domain.com:51444/Dashboard to see tenant1.
- Navigate to http://tenant2.domain.com:51444/Dashboard to see tenant2.
