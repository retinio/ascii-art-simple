# ascii-art-simple
Convert text file to ascii art banner

## Try it out!
```sh
$ chmod +x ascii_art.sh
$ cp ./data/lipsum.txt /tmp/lipsum.txt
$ ./ascii_art.sh -i /tmp/lipsum.txt
```
if you want to output ascii-art to a file use
```sh
$ ./ascii_art.sh -i /tmp/lipsum.txt -o /tmp/art.txt
```
## My test enviroment
```sh
$ cat /etc/os-release
NAME="CentOS Linux"
VERSION="8 (Core)"
ID="centos"
ID_LIKE="rhel fedora"
VERSION_ID="8"
PLATFORM_ID="platform:el8"
PRETTY_NAME="CentOS Linux 8 (Core)"
ANSI_COLOR="0;31"
CPE_NAME="cpe:/o:centos:centos:8"
HOME_URL="https://www.centos.org/"
BUG_REPORT_URL="https://bugs.centos.org/"

CENTOS_MANTISBT_PROJECT="CentOS-8"
CENTOS_MANTISBT_PROJECT_VERSION="8"
REDHAT_SUPPORT_PRODUCT="centos"
REDHAT_SUPPORT_PRODUCT_VERSION="8"
```
```sh
$ docker -v
Docker version 19.03.13, build 4484c46d9d
```

