# Hypothesis-Driven Threat Hunt: Unauthenticated RCE via a Groupware "ToolShed" Panel

**Graduate coursework — CIS*6580 Security Monitoring and Threat Hunting, University of Guelph**
**Individual project · PEAK Framework (Prepare, Execute, Act with Knowledge)**

## Overview

A full hypothesis-driven threat hunt (PEAK framework, Hunt ID H-0007) targeting exploitation of a
vulnerability advisory in a groupware "ToolShed" admin panel — an unauthenticated auth bypass
chained with OS command injection (CVSS 9.8). Rather than just writing a detection rule, this hunt
follows the complete lifecycle: build a testable hypothesis, scope it with the ABLE methodology
(Actor, Behavior, Location, Evidence), write and validate detections at both the host and network
layer, and generate synthetic attack traffic to confirm the detections actually fire on the real
exploitation pattern — and don't false-positive on lookalike benign traffic.

## The vulnerability

Two-stage exploit chain:
1. **Auth bypass** — a spoofed `Referer` header containing `SignOut.aspx` yields an unauthenticated
   session token
2. **Command injection** — a `__NIMBUSSTATE` form field with a raw (unencoded) semicolon and a
   `cmd=` action gets passed directly to `Runtime.exec("/bin/sh", "-c", cmd)` with no sanitization

On the host, this shows up as the application's Java process spawning a `/bin/sh -c` child running
attacker-controlled input — distinguishable from the one legitimate java-to-shell pattern in the
environment (an hourly self-check script) by command-line content.

Maps to MITRE ATT&CK **T1190** (Exploit Public-Facing Application) and **T1059.004** (Unix Shell).

## What's in this hunt

- **[`hunt-report/PEAK_Hunt_Report.pdf`](./hunt-report/PEAK_Hunt_Report.pdf)** — the full PEAK-framework
  hunt document: hypothesis statement, ABLE scoping, evidence sources with exact log fields and
  discriminators, hunting query logic, and confirming/refuting criteria defined *before* the hunt
  started (so the hypothesis was genuinely falsifiable, not fitted after the fact)
- **[`detections/sigma_rule.yml`](./detections/sigma_rule.yml)** — a Sigma detection rule for the
  host-layer process-creation pattern (java → `/bin/sh -c`), with the known-benign self-check
  explicitly excluded to avoid a false-positive-prone rule
- **[`detections/suricata.rules`](./detections/suricata.rules)** — two Suricata network rules
  detecting the auth-bypass request and the command-injection request independently, keyed to the
  exact byte-level discriminator (raw `;` vs. the benign traffic's percent-encoded `%3B`)
- **[`traffic-simulation/flowsynth.fs`](./traffic-simulation/flowsynth.fs)** — a Flowsynth script that
  synthesizes the two-stage exploit traffic from scratch, used to generate a test PCAP for validating
  the Suricata rules against realistic packet data rather than just log samples
- **[`traffic-simulation/generatedPCAP.pcap`](./traffic-simulation/generatedPCAP.pcap)** — the
  resulting synthetic packet capture of the exploit sequence

## Why this approach matters

A detection rule that's never validated against real traffic is a guess. This hunt closes that loop:
hypothesis → evidence requirements defined up front → host detection (Sigma) → network detection
(Suricata) → synthetic traffic generation to prove the rules actually fire on the attack and don't
fire on the one benign lookalike pattern in the environment. That last step — explicitly designing
against a false-positive source rather than discovering it in production — is the part most quick
detection write-ups skip.

## Stack

`PEAK Framework` · `MITRE ATT&CK` · `Sigma` · `Suricata` · `Flowsynth` · `auditd / Sysmon for Linux`
