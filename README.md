# RPN Calculator

This project provides a command line implementation of an RPN calculator.

## What is RPN?

RPN, or "Reverse Polish Notation", is a postfix notation for mathematical
operations. The common infix notation places the operator between the operands:

    1 + 2

RPN instead places the operator after the operands:

    1 2 +

This postfix notation eliminates the need to use parentheses to specify the
order of operations. For example, in the following expression using infix
notation:

    1 + 2 * 3

The multiplication is performed before the addition. To specify that the
addition must be performed first, the expression would need to be written as:

    (1 + 2) * 3

With RPN, the first expression becomes:

    1 2 3 * +

Whereas the second expression would be:

    1 2 + 3 *

## Implementation and operation

RPN calculators can be easily implemented using a stack. Each number entered
into the calculator is pushed onto the stack. Each binary operator (e.g. '+',
'*') pops the top two numbers from the stack, performs the calculation, then
pushes the result back on the stack.

For example, with the input:

    1 2 3 * +

The stack manipulations would appear as follows:

      1      2      3      *      +
      v      v      v      v      v

                  +---+
                  | 3 |
           +---+  +---+  +---+
           | 2 |  | 2 |  | 6 |
    +---+  +---+  +---+  +---+  +---+
    | 1 |  | 1 |  | 1 |  | 1 |  | 7 |
    +---+  +---+  +---+  +---+  +---+

Likewise, with the input:

    1 2 + 3 *

The stack manipulations would appear as:

      1      2      +      3      *
      v      v      v      v      v

           +---+         +---+
           | 2 |         | 3 |
    +---+  +---+  +---+  +---+  +---+
    | 1 |  | 1 |  | 3 |  | 3 |  | 9 |
    +---+  +---+  +---+  +---+  +---+

## Goal

Create an RPN calculator that takes a string input and performs the
corresponding action.

- If the input is a number, push the number on the stack.
- If the input is an operator, perform the operation.

The RPN calculator must maintain its stack between inputs. Assume all numbers
are integers.

### Steps

1. Update the `Enter()` method so that numbers are pushed onto the stack.
    - Strings should be parsed with `IntegerParser`.
2. Update the `Enter()` method to handle the addition operator: `+`. `Enter()`
   should throw an exception if there are not enough operands on the stack.
3. Update the `Enter()` method to handle the multiplication operator: `*`.
   `Enter()` should throw an exception if there are not enough operands on the
   stack.
4. Update the `Enter()` method so that unknown operations throw an exception.

### Bonus steps

1. Update `Enter()` to support the remaining basic operators:
    - subtraction: `-`
    - division: `/`
2. Update `Enter()` to support special directives:
    - Clear stack: `c`
    - Undo last stack operation: `u`
