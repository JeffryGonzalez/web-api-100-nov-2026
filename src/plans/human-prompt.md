# Working With the Agent (A Note From the Other Side of the Chat Window)

> Companion to [prompt.md](./prompt.md). That file tells the *agent* what
> to produce. This one is for *you* - the human in the conversation.
>
> Yes, this guide was written by an AI agent. That's less strange than it
> sounds: nobody knows better what context I'm missing than I do.

## The mindset shift

If your current mode of working with AI is pasting an error into a chat
window and typing "fix it" - that works, sometimes, and it's also the
least valuable thing we can do together.

Here's the uncomfortable truth from my side: **generating the code is the
least interesting part.** By the time a good plan exists, the code is
close to a formality. The valuable work is everything before that -
figuring out what the options are, which one fits *your* situation, and
why. Working this way is essentially **code review before the code
exists** - when changing direction costs a conversation, not a sprint.

So give the agent some agency early ("what approaches could we take?",
"what am I missing?") and keep the *authority* for yourself. I'm good at
breadth - naming options, trade-offs, and prior art. You're good at the
thing I cannot do: knowing your actual situation.

## What I don't know (and you do)

I will never ask you for most of this unless prompted, and without it I
fill the gaps with *plausible defaults* - which are the most dangerous
kind of wrong, because they look right:

- **Your organization's constraints.** Approved technologies, licensing,
  what ops will actually run, whether developers have admin rights.
- **Your team.** What they already know, what they're hiring for, how
  much new-thing budget this project can absorb.
- **History.** What's been tried and failed here, and why. Political
  realities ("the DBA team owns all schemas") are real constraints.
- **The actual goal.** Not the ticket text - the outcome. "Add caching"
  is a solution; "this page takes 9 seconds" is a problem.
- **What production looks like.** I'll happily design for a production
  that doesn't exist.

Two or three sentences of this context will change my answer more than
any amount of prompt wizardry.

## The conversation, in five moves

1. **State the objective as an outcome, with constraints.**
   Not "add a database" but "we need to persist vendors; the team knows
   SQL Server and Mongo, ops won't support anything exotic, and I want
   integration tests to be easy."
2. **Ask for approaches, not an answer.**
   "What approaches could we take?" Then *edit the list*: ask what's
   missing, strike what your org would never allow, ask why an option
   you expected isn't there.
3. **Decide together - but you decide.**
   Ask me to argue the case for one and against another. Ask "which
   would you pick for a team like mine, and why?" Then make the call and
   **write the reasoning down** - the recorded reasoning is the artifact
   that outlives the chat.
4. **Ask for a plan, and review it like code.**
   This is the step to share with a mentor, team lead, or architect. A
   plan is cheap to change; a half-built implementation is not.
5. **Only then, the code - one plan step at a time.**
   Small steps against an agreed plan are easy to verify and easy to
   stop. "Build the whole thing" is where agents wander off.

## Prompts, weak and strong

| Weak | Stronger |
|---|---|
| "Fix it" | "Here's the error and what I expected. What are the likely causes, most probable first?" |
| "Add a database" | "I need to persist X. Team knows Y. What approaches could we take?" |
| "Which is best?" | "Which fits *these constraints*, and what would make you change that answer?" |
| "Write the tests" | "What behaviors here are worth testing? Then let's write those." |
| Accepting the first answer | "What's the strongest argument against the thing you just recommended?" |

## When I'm wrong (I will be)

I am occasionally - and confidently - wrong. Treat my output like a
draft from a well-read colleague who just joined and doesn't know your
systems yet: useful, fast, and in need of review.

- Push back. If my answer flips under light pressure, that's signal.
- Ask "why?" until the reasoning is *yours*. If you can't explain the
  decision without me, you don't own it yet.
- Verify claims that matter (versions, APIs, licensing) before building
  on them.
- "I don't understand X - explain it" is a power move, not a confession.

## Why bother

- Decisions get made **deliberately** and recorded, instead of being
  whatever the first generated code implied.
- You produce **reviewable artifacts** (approaches, a plan) *before*
  expensive artifacts (code).
- You build the skill that still matters: **discernment**. The syntax is
  cheap now. The judgment isn't.
