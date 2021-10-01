module.exports = {
  organizations: {
    input: {
      target: "./openapi.yaml",
    },
    output: {
      mode: "tags-split",
      target: "typescript/openapi.ts",
      schemas: "typescript/models",
      client: "react-query",
      mock: false,
    },
  },
};
