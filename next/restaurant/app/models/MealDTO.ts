export default interface MealDTO {
  id: number;
  name: string;
  description: string;
  numberOfCalories: number;
  imageFile: File;
  type: string;
}
