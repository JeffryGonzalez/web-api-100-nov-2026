# Prompt: Create an "Approaches + Plan" Decision Record

> Usage: point an AI assistant at this file and say something like:
> *"Using `plans/prompt.md`, create a plans entry for 〈topic〉, e.g. 'persisting data', 'authentication', 'caching'."*

---

I'm teaching a class on building APIs with .NET to professional developers (mostly corporate Windows developers). A goal of this class is to build **discernment** - the ability to weigh real-world trade-offs - not just to memorize syntax. To support that, I keep lightweight decision records in the `plans/` directory.

I'd like you to create one of these for the topic I give you. Follow the structure and conventions below.

## Directory structure

Each topic gets a numbered directory (numbering follows the order topics appear in the class, in increments of 10):

```
plans/
  NN-topic-name/
    plan.md             <- the decision and the plan
    approaches/
      approach-one.md   <- one file per approach, kebab-case
      approach-two.md
```

## Step 1: Brainstorm the approaches

Identify roughly **4-7 realistic ways** a team could accomplish the goal. Include:

- The "obvious"/legacy way (even if we won't pick it) - students will encounter it at work.
- The current mainstream/community-standard way.
- At least one modern or emerging alternative.
- Approaches that are *valid but wrong for this class* - rejected options teach as much as chosen ones.

Don't pad the list with strawmen or hyper-niche tools the audience will never touch. Every approach should be something a reasonable team somewhere actually does.

If you're unsure whether an approach is in scope, ask me before writing it up.

## Step 2: Write each approach file

Each `approaches/*.md` file follows this shape:

```markdown
# Name of the Approach

One to three short paragraphs: what this approach *is*, how it works
mechanically, and where it sits historically/culturally (e.g. "this was
the default in most enterprises for decades").

## Advantages

- Punchy bullets. Each one names a benefit *and* why it matters.

## Disadvantages

- Honest ones - including for the approach we end up choosing.
- Include practical/classroom realities (resource limits, admin rights,
  licensing, learning curve) - not just technical purity.
```

Tone: conversational and plainspoken, like an experienced colleague talking, not marketing copy. A little humor is fine. Be fair to every approach - the goal is for a student to read the file and understand *when they would pick it*, even if we didn't.

Where approaches relate to each other (e.g. one is a managed version of another), say so - those connections build intuition.

## Step 3: Write `plan.md`

Structure:

```markdown
# 〈Goal, stated as a verb phrase〉

We are taking the [〈Chosen Approach〉](./approaches/chosen.md) approach

> **A classroom decision.** (Include this callout when it applies.) Some
> of our picks are made *for pedagogy* - least distracting, best for
> comparison, easiest to observe - and a real team might reasonably
> choose differently, or would avoid something we're doing on purpose
> (e.g. mixing two styles so students can compare them). Say so
> explicitly, and state what the "real project" norm would be.

## Approaches Considered

- Links to every approach file, with the pick marked ⬅️

## Reasoning

- Short bullets: the deciding factors *for this class/project*.
- Include at least one honest caveat (something the chosen approach is
  weak at that we're accepting).

## We'd Revisit This If

- Two to four *observable* triggers that would reopen this decision -
  an assumption in the reasoning stops being true, the team's context
  changes, a dependency stalls.

## Plan

A numbered sequence of steps to implement the chosen approach.

- General enough to apply to any project of this kind - name the *kind*
  of thing ("the API project", "your first resource"), not
  project-specific names.
- Each step: a bolded imperative sentence, then 1-2 sentences of *why*
  this step, or why this order.
- Include verification steps ("run it and confirm X before moving on") -
  don't let students build on something they haven't seen work.
- End with a one-sentence statement of the result: what we have when
  the plan is done.
```

The detailed, click-by-click version of the plan lives in the `labs/` directory - `plan.md` is the general strategy, the lab is the specific execution.

A note on the "We'd Revisit This If" section - it matters more than it looks. A decision that names its own invalidators invites challenge instead of deference. A plan is a *cache of the reasoning*, cheap to regenerate when the world changes - and caches are supposed to get invalidated. This is what keeps these documents plans (partial, heuristic, revisable) rather than becoming the thing they're meant to replace: a formalized "this is just how we do it."

## Step 4: Check the decision with me

If I haven't already told you which approach we're taking (or the reasoning), **ask me** rather than deciding for me - the decision and its reasoning are the teaching point.
