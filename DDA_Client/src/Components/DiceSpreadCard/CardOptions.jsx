import RollImageButton from "../RollImageButton/RollImageButton";
import { SplitButton } from "primereact/splitbutton";
import { SpeedDial } from "primereact/speeddial";
import { Toolbar } from "primereact/toolbar";
import { Tooltip } from "primereact/tooltip";

import { useMemo } from "react";

const CardOptions = ({
  spreadId,
  isRollingImage,
  setDalle3ImageUrlState,
  setIsRollingImage,
}) => {
  const calculateGeenrateButtonDisabled = useMemo(() => {
    const conditions = [isRollingImage];
    // * If any of the condition depenedencies are true, then the button should be disabled
    return conditions.some((condition) => condition);
  }, [isRollingImage]);

  const endContentitems = [
    {
      label: "Roll Image",
      icon: "pi pi-palette",
      template: (item) => {
        return (
          <RollImageButton
            id={spreadId}
            setDalle3ImageUrlState={setDalle3ImageUrlState}
            setIsRollingImage={setIsRollingImage}
          />
        );
      },
    },
  ];
  const endContent = (
    <>
      <SplitButton
        label="Generate"
        icon="pi pi-plus-circle"
        model={endContentitems}
        rounded
        raised
        severity="secondary"
        disabled={calculateGeenrateButtonDisabled}
      />
    </>
  );

  const centerContentItems = [
    {
      label: "Delete",
      icon: "pi pi-trash",
      command: () => {},
    },

    {
      label: "Share",
      icon: "pi pi-send",
      command: () => {},
    },
    {
      label: "Like",
      icon: "pi pi-heart",
      command: () => {},
    },
    {
      label: "Tags",
      icon: "pi pi-tags",
      command: () => {},
    },
  ];
  const centerContent = (
    <>
      <Tooltip target=".speeddialMenu .p-speeddial-action" position="bottom" />
      <SpeedDial
        model={centerContentItems}
        radius={80}
        type="circle"
        buttonClassName="p-button-secondary"
        className="speeddialMenu"
      />
    </>
  );

  return (
    <Toolbar
      center={centerContent}
      end={endContent}
      className="spreadCardOptions my-3"
    />
  );
};

export default CardOptions;
