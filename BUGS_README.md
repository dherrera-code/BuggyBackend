# Library System API - Bug Documentation

This is a buggy Library Management System API built with .NET and n-tier architecture. The system contains **20 intentional bugs** across different layers (Models, Repositories, Services, Controllers, and Configuration).

## Architecture

The application follows n-tier architecture:
- **Models Layer**: Data models (Book, Member, Transaction)
- **Repository Layer**: Data access logic
- **Service Layer**: Business logic
- **Controller Layer**: API endpoints
- **Program.cs**: Dependency injection configuration

---

## Please List all of the bugs you find & where you found it

Syntax / Compilation Errors: 2
Configuration Errors: 4
Logic Errors: 7
Validation Errors: 6
API Design Errors: 2

### EXMPLE **Bug #1: Wrong HTTP Request method in MemberController**
**Location**: `Controllers/MemberController.cs` - Line 38  
**Type**: API Design Error  

**Bug**:
```csharp
    [HttpDelete("{id}")]
    public ActionResult<Member> Update(int id, [FromBody] Member member)
```

## List of Bugs Docs

### 1. Change Targeting to .Net 9 instead of .Net 10
I didn't want to update my .Net to 10! In CSProj file, I changed Target Framework to 9.0 and reloaded window!

### 2. Syntax Error: MemberRepository.cs(23,69): error CS1010: Newline in constant
When initializing list of Members, the third entry was missing a quotation mark when entering email!

### 3. Logic Error: MemberRepository.cs(40,49): error CS0029: Cannot implicitly convert type 'string' to 'bool'!
The return for GetByEmail function needs to have double equals to compare members!

### 4. Logic error: \BookRepository.cs(37,47): error CS0029: Cannot implicitly convert type 'int' to 'bool'
GetById method in Book Repository needs to use double equals to find book with specified id!

### 5. Error: BooksController.cs(69,20): error CS0160: A previous catch clause already catches all exceptions of this or of a super type ('ArgumentException')
This error in the books controller has two catch blocks! I will comment our the second catch block to get rid of the error!

### 6. Config Error:  Unable to resolve service for type 'BuggyBackend.Repositories.IBookRepository' while attempting to activate 'BuggyBackend.Services.LibraryService'
This error doesn't host api because program.cs was missing a build services statement.
builder.Services.AddSingleton<IBookRepository, BookRepository>();

### 7. No Swagger configuration(Not really a bug) add swashbuckle packages via Nuget, configure launch settings and updated program.cs!


