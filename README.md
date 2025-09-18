# HyperMsg.Scada

A modular, cross-platform SCADA front-end powered by **Blazor**, **.NET MAUI**, and **HyperMsg.Core**. Designed for responsive telemetry visualization, control interfaces, and real-time communication across desktop, mobile, and cloud environments.

# Updating the Identity Database for the WebApi Project

To update the identity database using the `UserContext` class and the .NET CLI, follow these steps:

1. **Open a terminal in the WebApi project directory.**

2. **Update db with Identity migration migration:**

   ```bash
   dotnet ef database update InitialIdentityMigration -c UserContext
   ```
