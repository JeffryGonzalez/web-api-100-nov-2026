Advantages of using controllers:

- we have a TON of them. legacy.
- well understood

Must use reflection.

Also cause unneeded allocations - EVERY request creates a new instance of the controller, and those need to be GCd