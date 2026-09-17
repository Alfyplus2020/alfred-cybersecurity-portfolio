# Digital Forensics & Incident Response

**Graduate coursework — CIS*6520 Digital Forensics & Incident Response, University of Guelph**

Two independent forensic investigations, covering the two major branches of DFIR work: network
forensics from packet captures, and mobile-device forensics from a logical Android image. Both
follow the same discipline — verified evidence integrity, documented methodology, and a report
built to be defensible, not just technically correct.

## Investigations

- **[network-forensics-gtta-intrusion](./network-forensics-gtta-intrusion)** — A two-part case
  reconstructing a full intrusion lifecycle from PCAP evidence alone: a confirmed CVE-2024-4577
  perimeter compromise with coordinated ICS/OT reconnaissance, followed (10 weeks later) by the
  attacker returning internally to operate a planted webshell and exfiltrate data — with a
  second, hidden backdoor discovered along the way.

- **[mobile-forensics-android-case-study](./mobile-forensics-android-case-study)** — A mobile DFIR
  case study examining a logical Android device image: identity/device profiling, hacking-tool
  and anti-forensic-tool triage, communications analysis, web-history reconstruction, and
  GPS/EXIF geolocation analysis, closed out with a full mapping of evidence to specific alleged
  offences.

## Stack / Method

`Wireshark` · `Autopsy` · `aLEAPP` · `VirusTotal` · `AbuseIPDB` · `CyberChef` ·
`MITRE ATT&CK` / `MITRE ATT&CK for ICS` · Chain of custody & evidence-integrity verification
