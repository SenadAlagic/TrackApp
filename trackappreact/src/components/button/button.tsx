import React from "react";

function Button() {
  return (
    <button
      type="button"
      className="btn btn-primary"
      onClick={() => {
        console.log("clicked");
      }}
    >
      Klikni me
    </button>
  );
}

export default Button;
