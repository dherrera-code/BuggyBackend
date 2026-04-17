# Daniel Herrera

# 4/17/2026

# Buggy Backend

# Hosted Link:
https://buggyapiassignmentdh-acdpd8hjazfpckd7.westus3-01.azurewebsites.net/

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

### EXAMPLE **Bug #1: Wrong HTTP Request method in MemberController**
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
**Location**: repository/MemberRepository 
**type**: Syntax Error
When initializing list of Members, the third entry was missing a quotation mark when entering email!

### 3. Logic Error: MemberRepository.cs(40,49): error CS0029: Cannot implicitly convert type 'string' to 'bool'!
**Location**: repository/MemberRepository 
**type**: logic Error
The return for GetByEmail function needs to have double equals to compare members!

### 4. Logic error: \BookRepository.cs(37,47): error CS0029: Cannot implicitly convert type 'int' to 'bool'
GetById method in Book Repository needs to use double equals to find book with specified id!

### 5. Compiler Error: BooksController.cs(69,20): error CS0160: A previous catch clause already catches all exceptions of this or of a super type ('ArgumentException')
This error code (CS0160) shows the first derived class which is the argument null exception should be the first to be checked and the second derived exception should be afterwards! I basically swapped the two catch blocks to resolve the error!

### 6. Config Error:  Unable to resolve service for type 'BuggyBackend.Repositories.IBookRepository' while attempting to activate 'BuggyBackend.Services.LibraryService'
This error doesn't host api because program.cs was missing a build services statement.
builder.Services.AddSingleton<IBookRepository, BookRepository>();

### 7. No Swagger configuration(Not really a bug) add swashbuckle packages via Nuget, configure launch settings and updated program.cs!

### 8. Input Validation for creating member within controller
Moved input validation to the controller: MemberController! if(name or email) is empty or whitespaces! return bad request!

### 9. Input validation: Library endpoint transaction members/membersId
**Location**: libraryController And libraryService 
**type**: input validation
Change to return 400 for invalid member id!
In Library Services: GetMemberTransactions function test if member id exist before getting transaction. if member does not exist, return null and if transactions is null in the controller, it will return a bad request!

### 10. Bug where borrowedIds for created members have id 0 in Member controller!
Location: Repository/MemberRepository
Fix: MemberRepository:

member.BorrowedBookIds.Clear();

 When creating a new member, BorrowedBookIds are now empty upon creation instead of saving index 0 or any index. 

 ### 11. Validation Input: MemberService AND MemberRepository
 Created function in repository to find name of members and in memberService test if name already exist! If name already exist in the list, throw new InvalidOperationException

        var memberName = _memberRepository.GetByName(member.Name);
        if(memberName != null)
        {
            throw new InvalidOperationException("A member with this name already exists");
        }
## 12. Change DateTime.Now to DateTime.UtcNow when creating member, and transaction dates. Better performance. ()
Repository/ MemberRepository 
Repository/ TransactionRepository

## 13. Found bug that allows creation of the same book in the fake database list.
Location: BookService 
Added function to check if Book already exist before creating and saving an instance of a book into our database list!

## Notes to Test!
Test whether book copies are actually decrementing and incrementing!

Testing Members endpoints!! (Members endpoints seem to be working as intended!)

It seems like our controller endpoints are working as intended!
I am using the debug tool in vscode to look for any logic errors!