# SAST Testing Guide

## Purpose

This application contains a dedicated **SAST Demo page** (`/SASTDemo`) with intentional security vulnerabilities designed specifically for testing Static Application Security Testing (SAST) tools.

## ⚠️ Important Warning

**This page contains intentional security vulnerabilities for educational and testing purposes ONLY.** These vulnerabilities should never be used in production code and are clearly marked as "VULNERABILITY FOR SAST TESTING".

## How to Use for SAST Testing

### 1. Access the SAST Demo Page
Navigate to `http://localhost:5000/SASTDemo` (or `http://localhost:8080/SASTDemo` when running in Docker)

### 2. Run Your SAST Tool
Execute your SAST scanner against the project directory:
```bash
# Example with common SAST tools
sonar-scanner
# or
semgrep --config=auto .
# or
codeql database analyze . --format=csv --output=results.csv
```

### 3. Expected Vulnerability Detection

Your SAST tool should detect the following vulnerability types:

| Vulnerability Type | Location | Expected Finding |
|-------------------|----------|------------------|
| Hard-coded Credentials | `SASTDemo.cshtml.cs` | Database password, API keys, JWT secrets |
| SQL Injection | `SASTDemo.cshtml.cs` | String concatenation in SQL queries |
| Cross-Site Scripting (XSS) | `SASTDemo.cshtml.cs` | Unencoded user input in HTML output |
| Weak Hashing | `SASTDemo.cshtml.cs` | MD5 and SHA1 usage for password hashing |
| Insecure Cookies | `SASTDemo.cshtml.cs` | Missing HttpOnly and Secure flags |
| Hard-coded IP Addresses | `SASTDemo.cshtml.cs` | Static IP addresses in code |
| Debug Features | `SASTDemo.cshtml.cs` | Console output and stack trace exposure |
| CSRF Protection Disabled | `SASTDemo.cshtml` | Missing anti-forgery tokens |
| Excessive Content Length | `SASTDemo.cshtml` | Unlimited request size configuration |

## Vulnerability Examples

### 1. Hard-coded Credentials
```csharp
// VULNERABILITY FOR SAST TESTING
private readonly string dbConnection = "Server=localhost;Database=test;User=admin;Password=Password123!;";
```

### 2. SQL Injection
```csharp
// VULNERABILITY FOR SAST TESTING
public string GetUserData(string userId) {
    string query = "SELECT * FROM Users WHERE UserId = " + userId;
    return ExecuteQuery(query);
}
```

### 3. XSS Vulnerability
```csharp
// VULNERABILITY FOR SAST TESTING
public string DisplayUserInput(string userInput) {
    return "<div>User said: " + userInput + "</div>";
}
```

### 4. Weak Hashing
```csharp
// VULNERABILITY FOR SAST TESTING
public string HashPasswordMD5(string password) {
    using (var md5 = MD5.Create()) {
        // MD5 is cryptographically weak
    }
}
```

## Testing Checklist

Use this checklist to verify your SAST tool effectiveness:

- [ ] Detects hard-coded passwords and API keys
- [ ] Identifies SQL injection vulnerabilities
- [ ] Finds XSS vulnerabilities in HTML output
- [ ] Flags weak cryptographic algorithms (MD5, SHA1)
- [ ] Detects insecure cookie configurations
- [ ] Identifies hard-coded IP addresses
- [ ] Finds debug statements in production code
- [ ] Detects missing CSRF protection
- [ ] Identifies unlimited content length settings

## Best Practices for SAST Testing

1. **Run in Development**: Only use this demo page in development environments
2. **Isolate from Production**: Never deploy the SAST demo page to production
3. **Document Findings**: Keep records of what your SAST tool detects
4. **Compare Tools**: Use this demo to compare different SAST tools
5. **Regular Testing**: Periodically test your SAST tool configuration

## Security Considerations

- The vulnerable code is clearly marked with comments indicating it's for testing
- All vulnerabilities are contained within the SAST demo files
- The main application pages remain secure
- Docker configuration isolates the testing environment

## Common SAST Tools

Here are some popular SAST tools you can test with this demo:

- **SonarQube/SonarScanner**
- **Semgrep**
- **GitHub CodeQL**
- **Checkmarx**
- **Veracode**
- **Fortify SCA**
- **Bandit** (for Python)
- **ESLint Security Plugin**

## Expected Results

A comprehensive SAST tool should identify **all** the intentional vulnerabilities in this demo page. If your tool misses any of the listed vulnerabilities, consider:

1. Checking your tool configuration
2. Updating vulnerability rulesets
3. Ensuring proper file scanning permissions
4. Verifying tool compatibility with .NET/C#

## Support

For questions about this SAST testing demo or to report issues with vulnerability detection, refer to the tool documentation or security team guidelines.

---

**Remember**: This is a controlled testing environment. Never use these vulnerability patterns in actual production code.
