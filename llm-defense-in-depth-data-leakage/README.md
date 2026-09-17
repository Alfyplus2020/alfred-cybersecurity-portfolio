# LLM Defense-in-Depth: Data Leakage & Prompt Injection Analysis

**Graduate coursework — CIS*6510 Cybersecurity and Defense in Depth, University of Guelph**
**Group project · Alfred Amoah, Amalachukwu Azubike, Kokou Houmey, Victor Vezina**
**Winter 2026**

## Overview

A controlled, simulation-based security experiment testing whether layered defenses can protect an
LLM-enabled web application against two realistic attack classes: **cross-user data leakage** and
**prompt injection**. The team built a Flask-based prototype with a mock LLM engine and synthetic
multi-user data, then ran a full **2³ factorial experiment** — toggling three independent defense
layers on and off in every combination — to measure exactly which controls stop which attacks, and
what happens when they're combined.

This is a rigorous, quantified security evaluation, not a demo: eight configurations tested against
the same attack prompts, every event logged, results scored against a risk scale.

## The three defense layers tested

| Layer | Stage | What it does |
|---|---|---|
| **L1 — Context Isolation** | Pre-generation | Scopes which user records the model can see to the requester only |
| **L2 — Prompt Sanitization** | Pre-generation | Detects and defuses known prompt-injection patterns before they reach the model |
| **L3 — Response Redaction** | Post-generation | Scans model output and masks sensitive fields (emails, balances, tokens, IDs) that don't belong to the requesting user |

## Key findings

- **The undefended baseline failed 100% of the time** — both attacks succeeded on every attempt,
  confirming that safety instructions *inside* the LLM prompt provide no real protection on their own.
- **No single layer stops both attack types.** L1 alone blocks leakage but not injection; L2 alone
  blocks injection but not leakage; L3 alone masks output but doesn't stop injection from reaching
  the model.
- **All three layers together (L1+L2+L3) fully blocked both attacks** across every tested attempt —
  moving overall risk from "Critical" to "Near Zero" on the team's risk scale.
- Findings align with published research (OWASP LLM Top 10, NIST AI RMF, Microsoft's LLM-agent
  privacy research), reinforcing that the model itself should be treated as an untrusted component
  inside a secured application architecture — not the security boundary itself.

## My contribution

This was an equally collaborative team effort — we worked through the threat model, architecture,
and experiment design together. My specific role was **content and requirements lead**: identifying
which AI guardrails and security controls the system actually needed, and making sure the defense
layers we built mapped to real, recognized risks (grounded in OWASP's LLM Top 10 and the NIST
Generative AI Risk Management Profile) rather than an ad hoc list.

## Stack

`Python` · `Flask` · `HTML/CSS/JavaScript` · Regex-based sanitization/redaction · Structured logging
(CSV) · Git/GitHub

## Full report

[`report/CIS6510_Defense_in_Depth_Final_Report.pdf`](./report/CIS6510_Defense_in_Depth_Final_Report.pdf)
— includes full methodology, the eight-configuration results matrix, and demo screenshots
showing the system behavior with each layer toggled on/off.
