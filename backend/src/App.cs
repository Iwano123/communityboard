// Global settings
Globals = Obj(new
{
    debugOn = true,
    detailedAclDebug = false,
    aclOn = true,
    isSpa = true,
    port = args.Length > 0 ? args[0] : "5002",
    serverName = "Minimal API Backend",
    frontendPath = args.Length > 1 ? args[1] : "../dist",
    dbPath = args.Length > 2 ? args[2] : "_db.sqlite3",
    sessionLifeTimeHours = 2
});

// Initialize ACL rules
var aclTableExists = SQLQueryOne("SELECT name FROM sqlite_master WHERE type='table' AND name='acl'");
if (aclTableExists == null)
{
    // Create ACL table
    SQLQuery("CREATE TABLE acl (id INTEGER PRIMARY KEY AUTOINCREMENT, route TEXT, method TEXT, userRoles TEXT, allow TEXT, match TEXT)");
}

// Clear existing rules to prevent duplicates during setup
SQLQuery("DELETE FROM acl");

// Insert default ACL rules
// General disallow for /api for visitors (match=false means disallow if route does NOT match)
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api', 'GET', 'visitor,user,admin', 'allow', 'false')");
// Allow login for all
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/login', '*', 'visitor,user,admin', 'allow', 'true')");
// Allow user registration (POST to /api/users) for visitors
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/users', 'POST', 'visitor', 'allow', 'true')");
// Allow admin to manage users
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/users', '*', 'admin', 'allow', 'true')");
// Allow admin to manage ACL
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/acl', '*', 'admin', 'allow', 'true')");
// Allow admin to manage sessions
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/sessions', '*', 'admin', 'allow', 'true')");
// Disallow /secret.html for visitors and users
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/secret.html', 'GET', 'visitor,user', 'disallow', 'true')");
// Allow products for all
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/products', 'GET', 'visitor,user,admin', 'allow', 'true')");

// Add rules for /api/posts
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/posts', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/posts', 'POST', 'user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/posts', 'PUT', 'user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/posts', 'DELETE', 'admin', 'allow', 'true')");

// Add rules for /api/categories
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/categories', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/categories', 'POST', 'admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/categories', 'PUT', 'admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/categories', 'DELETE', 'admin', 'allow', 'true')");

// Add rules for /api/users (specific for logged-in users)
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/users', 'GET', 'user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/users', 'PUT', 'user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/api/users', 'DELETE', 'admin', 'allow', 'true')");

// Add rules for static files
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/css', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/js', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/images', 'GET', 'visitor,user,admin', 'allow', 'true')");
SQLQuery("INSERT INTO acl (route, method, userRoles, allow, match) VALUES ('/assets', 'GET', 'visitor,user,admin', 'allow', 'true')");

Console.WriteLine("ACL setup completed successfully!");

Server.Start();