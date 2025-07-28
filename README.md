# HyperMsg.Scada

A modular, cross-platform SCADA front-end powered by **Blazor**, **.NET MAUI**, and **HyperMsg.Core**. Designed for responsive telemetry visualization, control interfaces, and real-time communication across desktop, mobile, and cloud environments.

## 🔧 Architecture Overview

HyperMsg.Scada adopts a hybrid client strategy, supporting:
- **Blazor WebAssembly** for flexible deployment and PWA capabilities
- **Blazor Server** for high-performance real-time dashboards
- **.NET MAUI** for native mobile/desktop integration
- **HyperMsg.Core** as mediator for decoupled messaging
- **SignalR**, **gRPC-Web**, and **REST APIs** for external communication

## 🚀 Features

- 📡 **Device List & Details Views** with real-time telemetry
- 🎛️ **Modular Control Panels** for command and feedback loops
- 🧠 **ONNX-compatible diagnostics** (optional)
- 🔄 **HyperMsg.Core-driven messaging bus**
- 📈 **Responsive UI** across resolutions and platforms
- 🔐 **Authentication and Role-based UI rendering**
- ⚙️ **Sparkplug B / MQTT integration** for device state awareness