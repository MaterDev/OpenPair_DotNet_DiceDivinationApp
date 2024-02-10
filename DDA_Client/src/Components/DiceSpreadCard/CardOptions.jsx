
import { useRef } from "react";
import RollImageButton from "../RollImageButton/RollImageButton";
import { Button } from "primereact/button";
import { Menu } from "primereact/menu";
import { Menubar } from "primereact/menubar";
import { Toolbar } from "primereact/toolbar";

import { useMemo } from "react";

const CardOptions = ({
  spreadId,
  isRollingImage,
  setDalle3ImageUrlState,
  setIsRollingImage,
}) => {

  const menu = useRef(null);
  const calculateGeenrateButtonDisabled = useMemo(() => {
    const conditions = [isRollingImage];
    // * If any of the condition depenedencies are true, then the button should be disabled
    return conditions.some((condition) => condition);
  }, [isRollingImage]);
  
  const endContent = (
    <>
    <span className="p-buttonset">
    <Button
      className="p-button-text p-button-plain menuItemButton border-bluegray-200 bg-bluegray-100"
      icon="pi pi-fw pi-pencil"
      label='Workshop'
      size="small"
      raised
    />
    <RollImageButton
            disabled={calculateGeenrateButtonDisabled}
            id={spreadId}
            setDalle3ImageUrlState={setDalle3ImageUrlState}
            setIsRollingImage={setIsRollingImage}
          />
    </span>
    </>
  );

 
  
  const startContent = (
    <>
      <Button
      icon="pi pi-fw pi-cog"
      size="small"
      severity="secondary"
      rounded
      raised
    />
    </>
  );

 

  return (
    <Toolbar
      start={startContent}
      end={endContent}
      className="spreadCardOptions my-3"
      severity="secondary"
    />
  );
};

export default CardOptions;
