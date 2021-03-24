# Identity Access Management (IAM)

## **Introduction**

IAM offers the following features:

- Centralize control of your AWS account
- Shared access to your AWS account
- Granular permissions
- Identity federation
- Multi Factor Authentication
- Provide temporary access for users/devices and services where necessary
- Allow you to setup your own password policy
- Integrates with many different AWS services
- Supports PCI DSS compliance

Considering the following terms:

- Users: End users such as people, employees of an organization, etc.
- Groups: A collection of users. Each user in the group will inherit the permissions of the group.
- Policies: Policies are made of JSON formatted documents called Policy documents which describes permissions to a User/Group/Role.
- Roles: Create roles and assign them to AWS services.

IAM is universal, which means that it currently does not apply to specific region.

The **root account** is simply the account created when first setup your AWS account, which has completely admin access.

New users do not have any permissions; on the other hand, they are assigned **Access Key ID** and **Secret Access Keys** when first created. Remember that **Access Key ID** and **Secret Access Keys**, which are used to access AWS via the APIs and Command Lines, are not equal to password and the administrator could only view them once or you have to regenerate them.

For security concerns, you should always generate Multi Factor Authentication on root account.
