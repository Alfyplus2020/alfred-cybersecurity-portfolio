# Alfred Amoah — Cybersecurity Portfolio

Selected work from my Master's in Cybersecurity and Threat Intelligence at the
University of Guelph, plus independent projects. Each subfolder is a self-contained
project with its own README covering the problem, my contribution, and the stack used.

## Projects

- **[apt29-threat-intel-malware-classification](./apt29-threat-intel-malware-classification)**
  — End-to-end threat intel pipeline: APT29 intelligence profiling, hash-verified
  malware collection, Ghidra-based opcode extraction, and malware family classification
  with both classical ML (k-NN/Decision Tree/SVM) and a CNN model. Team project with
  Victor Vezina.

- **[llm-defense-in-depth-data-leakage](./llm-defense-in-depth-data-leakage)**
  — Designed and ran a full 2³ factorial security experiment on an LLM-enabled web app,
  testing three defense layers (context isolation, prompt sanitization, response
  redaction) against cross-user data leakage and prompt injection attacks. Team project
  with Amalachukwu Azubike, Kokou Houmey, and Victor Vezina.

- **[threat-hunting-peak-toolshed](./threat-hunting-peak-toolshed)**
  — Individual hypothesis-driven threat hunt (PEAK framework) for an unauthenticated
  RCE vulnerability chain. Built matching host (Sigma) and network (Suricata)
  detections, then generated synthetic attack traffic with Flowsynth to validate both
  against the real exploit pattern and a benign lookalike.

- **[genai-polymorphic-malware-literature-review](./genai-polymorphic-malware-literature-review)**
  — A 55-source structured literature review on generative-AI-driven polymorphic malware:
  a two-axis threat taxonomy, a critical assessment of why current detection approaches
  (static, behavioral, semantic/graph-based, explainable AI) fall short against AI-generated
  variants, and a governance gap analysis across NIST AI RMF, ISO/IEC 42001, the EU AI Act,
  and MITRE ATLAS. Team project with Amalachukwu Azubike, Bereni Iyagba, and Kokou Houmey;
  the team is pursuing publication.

More projects will be added here as coursework and independent work wrap up.

## Contact

Guelph, Ontario, Canada · [LinkedIn — add your profile URL here]
