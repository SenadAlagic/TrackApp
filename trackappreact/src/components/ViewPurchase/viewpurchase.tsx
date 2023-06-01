import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { StyledWrapper } from "../../styles/wrapper.styled";
import { fetchPurchaseById } from "../../services/purchaseService";

interface PurchaseRaw {
  purchaseId: number;
  itemId: number;
  dateOfPurchase: number;
  purchasedBy: string;
  quantity: number;
  price: number;
  isVisible: boolean;
  imageBytes: string;
}
function ViewPurchase() {
  const { purchaseId } = useParams();
  const [image, setImage] = useState<string>();

  useEffect(() => {
    fetchPurchaseById(parseInt(purchaseId || "")).then((r: PurchaseRaw) => {
      setImage(r.imageBytes);
    });
  }, []);

  return (
    <>
      <StyledWrapper>
        <img
          src={`data:image/png;base64, ${image}`}
          alt="slika bi trebala biti tu"
        />
      </StyledWrapper>
    </>
  );
}

export default ViewPurchase;
