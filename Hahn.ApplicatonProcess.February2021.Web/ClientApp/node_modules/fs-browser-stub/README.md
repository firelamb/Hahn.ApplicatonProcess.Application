# fs-browser-stub [![Build Status](https://travis-ci.org/dumberjs/fs-browser-stub.svg?branch=master)](https://travis-ci.org/dumberjs/fs-browser-stub)

Stub node fs module in browser.

`fs.readFile` is still a function, would not throw an error.
Only when user calls `fs.readFile()`, it then throw an error.

# What this resolve?

By default, browserify/webpack/parcel/fusebox/dumber would stub `fs` module with nothing or an empty object `{}`.

I was confused why `@babel/core` v7.8.0+ does not work in browser anymore. It turns out the babel uses `gensync` and that checks the type of `sync` and `errback` options. It rejects if they are not `function`.
This new `fs` stub bypasses the issue.

From https://github.com/babel/babel/blob/master/packages/babel-core/src/gensync-utils/fs.js

```js
export const readFile = gensync<[string, "utf8"], string>({
  sync: fs.readFileSync,
  errback: fs.readFile,
});
```
