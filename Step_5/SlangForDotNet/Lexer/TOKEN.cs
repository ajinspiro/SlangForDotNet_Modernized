namespace SlangForDotNet.Lexer;

/// <summary>
/// Enum for Tokens
/// </summary>
public enum TOKEN
{
    ILLEGAL_TOKEN = -1, // Not a Token
    TOK_PLUS = 1, // '+'
    TOK_MUL, // '*'
    TOK_DIV, // '/'
    TOK_SUB, // '-'
    TOK_OPAREN, // '('
    TOK_CPAREN, // ')'
    TOK_NULL, // End of string
    TOK_PRINT, // Print Statement
    TOK_PRINTLN, // PrintLine
    TOK_UNQUOTED_STRING, // Variable name , Function name etc
    TOK_SEMI, // ; 

    //---------- Addition in Step 4

    TOK_VAR_NUMBER,         // NUMBER data type
    TOK_VAR_STRING,         // STRING data type 
    TOK_VAR_BOOL,           // Bool data type
    TOK_NUMERIC,            // [0-9]+ 
    TOK_COMMENT,            // Comment Token ( presently not used )   
    TOK_BOOL_TRUE,          // Boolean TRUE
    TOK_BOOL_FALSE,         // Boolean FALSE
    TOK_STRING,             // String Literal
    TOK_ASSIGN              // Assignment Symbol =  
}