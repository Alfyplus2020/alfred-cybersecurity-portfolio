# Android Device Forensic Investigation (Mobile DFIR Case Study)

**Graduate coursework — CIS*6520 Digital Forensics & Incident Response, University of Guelph**
**Individual project · Mobile (Android) forensic examination**

## Overview

A fictional criminal case built for teaching mobile forensics: a logical Android device image
("suspect" device) provided for examination against three simulated Criminal Code of Canada
offences (unauthorized use of a computer, mischief in relation to data, and interception of
communications). The exercise mirrors how a digital forensics examiner would actually receive and
work a case — evidence handed over with a published hash to verify, a defined scope, and a
requirement to present findings as an evidentiary report rather than a raw data dump.

All names, the device, and the alleged case are course-constructed fictional scenario material —
this project showcases the forensic examination methodology and reporting standard, not any real
investigation.

## What the examination covered

- **Evidence integrity first:** verified the device image's SHA-256 hash against the published
  value before any analysis, and documented chain of custody throughout — standard practice for
  anything that could be presented as evidence.
- **Identity and device profiling:** recovered and cross-referenced account identifiers (Google
  account, JWT tokens, SIM/carrier metadata) to build a confirmed identity and device profile,
  including reasoning through corroborating details (e.g., a phone area code matching a Gmail
  address suffix) rather than accepting a single data point.
- **Installed-application triage:** categorized 370 recovered application entries down to the ones
  of forensic interest — separating penetration-testing/hacking tools (Termux, Magisk, ADB-over-network)
  from anti-forensic tools (an EXIF-stripping app) from ordinary communication apps.
- **Communications analysis:** recovered and interpreted SMS/MMS thread content from the device's
  messaging database, reading past casual language to flag operationally significant lines
  (coded terms, recruitment conversations, an app-verification-code pattern indicating deliberate
  multi-account creation).
- **Web-history reconstruction:** rebuilt a multi-month browsing campaign from fragmented search
  and visit records to show *intent and technical progression* over time — from installing an
  exploitation framework, to reconnaissance against a specific target, to researching advanced
  persistence techniques.
- **Photo/EXIF and geolocation analysis:** extracted GPS metadata across dozens of photos to
  reconstruct a multi-country travel timeline, cross-referenced a recovered deleted photo, and
  identified a temp file that directly proved active anti-forensic metadata manipulation — a
  "smoking gun" artifact connecting a suspicious web search to actual device activity.
- **Legal charge-mapping:** closed the loop by mapping each recovered artifact directly to the
  specific statutory element it supports, the standard a forensic report needs to be usable by
  a prosecutor rather than just a technically-accurate writeup.

## Why this project matters

Mobile forensics is a distinct skill from network/host forensics: everything comes from structured
app databases, metadata, and fragments rather than a live packet stream, and the analytical
challenge is correlation — tying together identity, communications, location, and installed
software into one coherent, evidence-backed narrative. This project also reinforced report
discipline: an evidentiary report has to separate what the data *shows* from what it *suggests*,
and justify every inferential leap.

## Full report

[`report/Android_Device_Forensic_Investigation_Report.pdf`](./report/Android_Device_Forensic_Investigation_Report.pdf)

## Stack / Method

`Autopsy 4.22.1` · `Android Analyzer (aLEAPP)` · `Picture Analyzer` (EXIF/GPS extraction) ·
`DB Browser for SQLite` · SHA-256 evidence-integrity verification · Structured DFIR reporting
(chain of custody, charge-mapping to statute)
