# Network Forensic Investigation: Perimeter Intrusion & Post-Exploitation (Simulated Transit Authority Case)

**Graduate coursework — CIS*6520 Digital Forensics & Incident Response, University of Guelph**
**Individual project · Two-part network forensic investigation**

## Overview

A two-part DFIR case study built around a fictional transit authority ("GTTA"), reconstructing a
complete intrusion lifecycle purely from packet captures — no host access, no live system, just
PCAP files and a structured forensic methodology. Case 001 covers the external perimeter
compromise; Case 002, captured ten weeks later, covers the attacker returning to the *internal*
network to run hands-on post-exploitation commands and exfiltrate data through a webshell planted
during Case 001 — establishing a single, evidence-linked attack chain across both captures.

This is packet-level forensic analysis and incident reporting, not offensive security: every
finding here was produced by dissecting already-captured traffic in Wireshark, decoding what was
found, and cross-referencing it against threat-intel sources (VirusTotal, AbuseIPDB) — the same
skill set used in a SOC/DFIR analyst or GRC incident-response role.

## Case 001 — Perimeter Intrusion (December 31, 2025)

Analysis of a 506,832-packet capture at a simulated transit authority's network perimeter.

- **Confirmed exploitation of CVE-2024-4577** (PHP-CGI argument injection, CVSS 9.8): traced the
  full two-stage exploit sequence in the raw TCP stream, decoded the Base64-encoded PHP payload,
  and confirmed server compromise from the HTTP 200 response and attacker-controlled content
  subsequently served by the victim server.
- Identified and evidenced six additional attack techniques observed in the same capture
  (Apache path traversal, PHPUnit RCE probing, Hikvision camera RCE, GeoServer RCE probing,
  systematic `.env` credential harvesting, and AI/LLM API-key abuse).
- Detected and documented a **coordinated ICS/OT reconnaissance campaign** — OMRON FINS, BACnet,
  and EtherNet/IP protocol scanning from overlapping infrastructure — assessed against what those
  protocols control in a transit environment (signalling, HVAC, fare gates).
- Threat-intel enrichment of every indicator (attacker IPs, C2 server, malicious domain) via
  VirusTotal and AbuseIPDB, including reasoning through a stale confidence score caused by
  AbuseIPDB's time-decay algorithm rather than taking it at face value.
- Full MITRE ATT&CK (Enterprise + ICS) technique mapping and a tiered, prioritized remediation plan
  (immediate/24-hour, short-term/7-day, ICS-specific, and strategic recommendations).

## Case 002 — Webshell Exploitation & Data Exfiltration (March 14, 2026)

A focused, 171-packet internal capture showing the attacker returning **from inside the network**
to operate the webshell planted during Case 001.

- Reconstructed the full attacker session command-by-command (`whoami` → `finger` → `ls` →
  `cat secret.txt`) by following TCP streams and decompressing gzip-encoded HTTP response bodies to
  recover plaintext command output.
- Identified the specific webshell (`badshell.php`, a variant of the publicly known "Simple PHP
  Backdoor") and, critically, spotted a **second, hidden backdoor** (`.reverse_shell.php`) in a
  directory listing — evidence of deeper persistence than the initial webshell alone suggested.
- Confirmed **data exfiltration** as a completed fact, not a risk: the attacker successfully read
  and received the contents of a file explicitly marked sensitive.
- Built a cross-case evidence table linking Case 002 directly back to Case 001 — same attack
  infrastructure, ~10-week gap, and a documented shift from an external to an internal attacker
  position, i.e. confirmed lateral movement.

## What's included here vs. left out

- **Included:** both full investigation reports (methodology, evidence, timelines, IOC tables,
  ATT&CK mappings, remediation), the Case 002 PCAP (small, internal-only capture), and filtered
  HTTP-transaction CSV exports for both cases.
- **Left out:** the ~44 MB Case 001 perimeter PCAP (500K+ packets of largely automated internet
  background-scan noise) — the report and CSV export capture everything analytically relevant;
  the full capture is available on request.

## Stack / Method

`Wireshark` (protocol hierarchy statistics, TCP stream reconstruction, gzip entity-body
decompression) · `VirusTotal` · `AbuseIPDB` · `CyberChef` (Base64 decoding) · `MITRE ATT&CK` &
`MITRE ATT&CK for ICS` · Structured DFIR reporting (chain of custody, evidence integrity, forensic
opinion)
