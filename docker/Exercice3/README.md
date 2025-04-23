docker build -t python-server .

docker scout cves python-server

Les erreurs :

PS C:\GitSrcIsitech\Devops\mars-rover\docker\Exercice3> docker scout cves python-server
i New version 1.17.1 available (installed version is 1.17.0) at https://github.com/docker/scout-cli
v Image stored for indexing  
v Indexed 169 packages
! failed to delete temporary image archive C:\Users\antho\AppData\Local\Temp\docker-scout\sha256\2b7d577716d9646c1dc34583a4a22f330e51d5b93194a38a26755392eac78a46\60a2e3dd-9076-4969-9bd6-da3f880750bd: remove C:\Users\antho\AppData\Local\Temp\docker-scout\sha256\2b7d577716d9646c1dc34583a4a22f330e51d5b93194a38a26755392eac78a46\60a2e3dd-9076-4969-9bd6-da3f880750bd: The process cannot access the file because it is being used by another process.    x Detected 18 vulnerable packages with a total of 35 vulnerabilities


## Overview

                    │       Analyzed Image
────────────────────┼──────────────────────────────
Target            │  python-server:latest
digest          │  2b7d577716d9
platform        │ linux/amd64
vulnerabilities │    0C     3H     2M    30L
size            │ 58 MB
packages        │ 169


## Packages and Vulnerabilities

0C     2H     0M     0L  setuptools 58.1.0
pkg:pypi/setuptools@58.1.0

    x HIGH CVE-2022-40897 [Inefficient Regular Expression Complexity]
      https://scout.docker.com/v/CVE-2022-40897
      Affected range : <65.5.1
      Fixed version  : 65.5.1
      CVSS Score     : 8.7
      CVSS Vector    : CVSS:4.0/AV:N/AC:L/AT:N/PR:N/UI:N/VC:N/VI:N/VA:H/SC:L/SI:L/SA:N

    x HIGH CVE-2024-6345 [Improper Control of Generation of Code ('Code Injection')]
      https://scout.docker.com/v/CVE-2024-6345
      Affected range : <70.0.0
      Fixed version  : 70.0.0
      CVSS Score     : 7.5
      CVSS Vector    : CVSS:4.0/AV:N/AC:L/AT:P/PR:N/UI:A/VC:H/VI:H/VA:H/SC:N/SI:N/SA:N


0C     1H     0M     0L  flask 2.0.1
pkg:pypi/flask@2.0.1

    x HIGH CVE-2023-30861 [Use of Persistent Cookies Containing Sensitive Information]
      https://scout.docker.com/v/CVE-2023-30861
      Affected range : <2.2.5
      Fixed version  : 2.2.5
      CVSS Score     : 8.7
      CVSS Vector    : CVSS:4.0/AV:N/AC:L/AT:N/PR:N/UI:N/VC:H/VI:N/VA:N/SC:N/SI:N/SA:N


0C     0H     1M     3L  krb5 1.20.1-2+deb12u2
pkg:deb/debian/krb5@1.20.1-2%2Bdeb12u2?os_distro=bookworm&os_name=debian&os_version=12

    x MEDIUM CVE-2025-3576
      https://scout.docker.com/v/CVE-2025-3576
      Affected range : >=1.20.1-2+deb12u2
      Fixed version  : not fixed

    x LOW CVE-2024-26461
      https://scout.docker.com/v/CVE-2024-26461
      Affected range : >=1.20.1-2+deb12u2
      Fixed version  : not fixed

    x LOW CVE-2024-26458
      https://scout.docker.com/v/CVE-2024-26458
      Affected range : >=1.20.1-2+deb12u2
      Fixed version  : not fixed

    x LOW CVE-2018-5709
      https://scout.docker.com/v/CVE-2018-5709
      Affected range : >=1.20.1-2+deb12u2
      Fixed version  : not fixed


0C     0H     1M     0L  pip 23.0.1
pkg:pypi/pip@23.0.1

    x MEDIUM CVE-2023-5752 [Improper Neutralization of Special Elements used in a Command ('Command Injection')]
      https://scout.docker.com/v/CVE-2023-5752
      Affected range : <23.3
      Fixed version  : 23.3
      CVSS Score     : 6.8
      CVSS Vector    : CVSS:4.0/AV:L/AC:L/AT:N/PR:L/UI:N/VC:N/VI:H/VA:N/SC:N/SI:N/SA:N


0C     0H     0M     7L  glibc 2.36-9+deb12u10
pkg:deb/debian/glibc@2.36-9%2Bdeb12u10?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2019-9192
      https://scout.docker.com/v/CVE-2019-9192
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2019-1010025
      https://scout.docker.com/v/CVE-2019-1010025
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2019-1010024
      https://scout.docker.com/v/CVE-2019-1010024
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2019-1010023
      https://scout.docker.com/v/CVE-2019-1010023
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2019-1010022
      https://scout.docker.com/v/CVE-2019-1010022
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2018-20796
      https://scout.docker.com/v/CVE-2018-20796
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed

    x LOW CVE-2010-4756
      https://scout.docker.com/v/CVE-2010-4756
      Affected range : >=2.36-9+deb12u10
      Fixed version  : not fixed


0C     0H     0M     4L  systemd 252.36-1~deb12u1
pkg:deb/debian/systemd@252.36-1~deb12u1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2023-31439
      https://scout.docker.com/v/CVE-2023-31439
      Affected range : >=252.36-1~deb12u1
      Fixed version  : not fixed

    x LOW CVE-2023-31438
      https://scout.docker.com/v/CVE-2023-31438
      Affected range : >=252.36-1~deb12u1
      Fixed version  : not fixed

    x LOW CVE-2023-31437
      https://scout.docker.com/v/CVE-2023-31437
      Affected range : >=252.36-1~deb12u1
      Fixed version  : not fixed

    x LOW CVE-2013-4392
      https://scout.docker.com/v/CVE-2013-4392
      Affected range : >=252.36-1~deb12u1
      Fixed version  : not fixed


0C     0H     0M     3L  perl 5.36.0-7+deb12u1
pkg:deb/debian/perl@5.36.0-7%2Bdeb12u1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2024-56406
      https://scout.docker.com/v/CVE-2024-56406
      Affected range : <5.36.0-7+deb12u2
      Fixed version  : 5.36.0-7+deb12u2

    x LOW CVE-2023-31486
      https://scout.docker.com/v/CVE-2023-31486
      Affected range : >=5.36.0-7+deb12u1
      Fixed version  : not fixed

    x LOW CVE-2011-4116
      https://scout.docker.com/v/CVE-2011-4116
      Affected range : >=5.36.0-7+deb12u1
      Fixed version  : not fixed


0C     0H     0M     2L  libgcrypt20 1.10.1-3
pkg:deb/debian/libgcrypt20@1.10.1-3?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2024-2236
      https://scout.docker.com/v/CVE-2024-2236
      Affected range : >=1.10.1-3
      Fixed version  : not fixed

    x LOW CVE-2018-6829
      https://scout.docker.com/v/CVE-2018-6829
      Affected range : >=1.10.1-3
      Fixed version  : not fixed


0C     0H     0M     2L  gcc-12 12.2.0-14
pkg:deb/debian/gcc-12@12.2.0-14?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2023-4039
      https://scout.docker.com/v/CVE-2023-4039
      Affected range : >=12.2.0-14
      Fixed version  : not fixed

    x LOW CVE-2022-27943
      https://scout.docker.com/v/CVE-2022-27943
      Affected range : >=12.2.0-14
      Fixed version  : not fixed


0C     0H     0M     1L  apt 2.6.1
pkg:deb/debian/apt@2.6.1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2011-3374
      https://scout.docker.com/v/CVE-2011-3374
      Affected range : >=2.6.1
      Fixed version  : not fixed


0C     0H     0M     1L  tar 1.34+dfsg-1.2+deb12u1
pkg:deb/debian/tar@1.34%2Bdfsg-1.2%2Bdeb12u1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2005-2541
      https://scout.docker.com/v/CVE-2005-2541
      Affected range : >=1.34+dfsg-1.2+deb12u1
      Fixed version  : not fixed


0C     0H     0M     1L  gnutls28 3.7.9-2+deb12u4
pkg:deb/debian/gnutls28@3.7.9-2%2Bdeb12u4?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2011-3389
      https://scout.docker.com/v/CVE-2011-3389
      Affected range : >=3.7.9-2+deb12u4
      Fixed version  : not fixed


0C     0H     0M     1L  util-linux 2.38.1-5+deb12u3
pkg:deb/debian/util-linux@2.38.1-5%2Bdeb12u3?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2022-0563
      https://scout.docker.com/v/CVE-2022-0563
      Affected range : >=2.38.1-5+deb12u3
      Fixed version  : not fixed


0C     0H     0M     1L  gnupg2 2.2.40-1.1
pkg:deb/debian/gnupg2@2.2.40-1.1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2022-3219
      https://scout.docker.com/v/CVE-2022-3219
      Affected range : >=2.2.40-1.1
      Fixed version  : not fixed


0C     0H     0M     1L  sqlite3 3.40.1-2+deb12u1
pkg:deb/debian/sqlite3@3.40.1-2%2Bdeb12u1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2021-45346
      https://scout.docker.com/v/CVE-2021-45346
      Affected range : >=3.40.1-2+deb12u1
      Fixed version  : not fixed


0C     0H     0M     1L  openssl 3.0.15-1~deb12u1
pkg:deb/debian/openssl@3.0.15-1~deb12u1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2010-0928
      https://scout.docker.com/v/CVE-2010-0928
      Affected range : >=3.0.11-1~deb12u2
      Fixed version  : not fixed


0C     0H     0M     1L  shadow 1:4.13+dfsg1-1
pkg:deb/debian/shadow@1%3A4.13%2Bdfsg1-1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2007-5686
      https://scout.docker.com/v/CVE-2007-5686
      Affected range : >=1:4.13+dfsg1-1
      Fixed version  : not fixed


0C     0H     0M     1L  coreutils 9.1-1
pkg:deb/debian/coreutils@9.1-1?os_distro=bookworm&os_name=debian&os_version=12

    x LOW CVE-2017-18018
      https://scout.docker.com/v/CVE-2017-18018
      Affected range : >=9.1-1
      Fixed version  : not fixed



35 vulnerabilities found in 18 packages
CRITICAL  0
HIGH      3
MEDIUM    2
LOW       30


What's next:
View base image update recommendations → docker scout recommendations python-server:latest
