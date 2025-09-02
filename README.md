
---

```markdown
# 🚀 **AZ-Multi-Template-Function Project**

This repository contains multiple Azure Function projects built with **.NET 9** and the **Azure Functions CLI**. It showcases various trigger types and a shared library for reusable components.



## 📁 **Projects Included**

- `SharedLib`: Common utilities and DTOs  
- `HttpTriggerFunction`: HTTP-triggered function  
- `TimerTriggerFunction`: Scheduled function  
- `QueueTriggerFunction`: Queue-triggered function  
- `BlobTriggerFunction`: Blob-triggered function  
```


📦 1. Add a SharedLib Project (Reusable Code)
Create a class library project to hold DTOs, utilities, and shared logic:
```bash
dotnet new classlib -n SharedLib -f net9.0
```
Then reference it from each function project:
```bash
cd HttpTriggerFunction
dotnet add reference ../SharedLib/SharedLib.csproj
```
---

## 🧪 **How to Run Locally**

### ▶️ Run a Function App

```bash
cd HttpTriggerFunction
func start
```

---

### 🔧 **Install Azure Functions Core Tools**

Install globally using npm:

```bash
npm install -g azure-functions-core-tools@4 --unsafe-perm true
```

---

### 🧪 **Install Azurite (Local Azure Storage Emulator)**

Azurite simulates Azure Blob, Queue, and Table services locally.

```bash
npm install -g azurite
```

Start all services:

```bash
azurite --blobHost --queueHost --tableHost
```

---

### 🏗️ **Create a New Function App Project**

```bash
func init MyFunctionApp --worker-runtime dotnet-isolated --language C#
cd MyFunctionApp
dotnet build
```

Or target .NET 9 directly:

```bash
func init HttpTriggerFunction --worker-runtime dotnetIsolated --target-framework net9.0
```

---

### ✨ **Add a New Function to the Project**

List available templates:

```bash
func templates list
func templates list --language C#
```

Create a new HTTP-triggered function:

```bash
func new --name HttpExample --template "HTTP trigger" --authlevel "anonymous"
```

Create a new Timer-triggered function:

```bash
func new --name TimerDemo --template "Timer trigger"
    OR
func new --name TimerDemo --template "Timer trigger" --force
---

### 🚀 **Run the Function App Locally**

```bash
func start
```

---

### 🧪 **Test with CURL**

```bash
curl http://localhost:7071/api/HttpExample?name=YourName
```

---

## 🛠️ **Basic CURL Operations**

- **Fetch a URL**:  
  `curl <URL>`

- **Download a File**:  
  `curl -O <URL_of_file>`

- **Follow Redirects**:  
  `curl -L <URL>`

- **Verbose Mode**:  
  `curl -v <URL>`

- **Retrieve Only Headers**:  
  `curl -I <URL>`

## Git Ignore
Use the CLI to generate a solid .gitignore:
```bash
dotnet new gitignore
```
---
## 📚 **CI/CD Deployment using GitHub -> Action having 2 Approach**
✍️ Option 1: Create It Manually in VS Code
This is the most flexible and developer-friendly way.
✅ Steps:
    1. In your project root, create the folders:
mkdir -p .github/workflows
    2. Inside .github/workflows, create a new file:
touch ci.yml
    3. Open ci.yml in VS Code and paste your CI workflow (like the one I shared earlier).
    4. Save and commit:
git add .github/workflows/ci.yml
git commit -m "Add CI workflow for Azure Functions"
git push origin main

⚙️ Option 2: Use GitHub → Actions → Set Up Workflow
If you prefer a guided setup:
    1. Go to your GitHub repo in the browser.
    2. Click the Actions tab.
    3. GitHub will suggest some starter workflows. You can:
        ○ Choose "set up a workflow yourself"
        ○ Or select a template like .NET Core, then customize it
    4. GitHub will create a .github/workflows/main.yml file for you.
    5. Edit it directly in the browser or pull it locally to rename and modify.
