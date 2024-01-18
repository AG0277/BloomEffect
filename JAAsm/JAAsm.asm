.data   
    myFloat1 REAL4 3.00
    thresholdValue REAL4 191.00
    myInt dd 4 
    two dd 2
    radiusLenght dd 0
    myImmediateValue dq 12345678912
    rgbaMask db 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0  ; RGBA mask for blending
.code

MyProc1 proc

    xor rax, rax            ; Clear rax to store temporary values
    mov rbx, r8 
    mov r10d, [myInt]
    imul rbx, r10
         ; passed in rcx originalPixels,
         ; passed in rdx  bloomPixels,   
         ; passed in r8  width,  
         ; passed in r9  height 
         ; r10 bytesperpixel 

    outer_loop:
        mov R11, 0          ; Clear r11 to store resultIndex
        xor rax, rax   
        inner_loop:
            movzx r12, byte ptr [rcx  + r11]           
            movzx r13, byte ptr [rcx  + r11 + 1]       
            movzx r14, byte ptr [rcx  + r11 + 2]       

            add rax, r12
            add rax, r13
            add rax, r14

            cvtsi2ss xmm1, rax
            movss xmm0, dword ptr [myFloat1]
            divss xmm1, xmm0

            movss xmm2, dword ptr [thresholdValue]
            comiss xmm1, xmm2
            ja greater_than_191
            mov byte ptr [rdx + r11] , 0
            mov byte ptr [rdx + r11 + 1] , 0
            mov byte ptr [rdx + r11 + 2] , 0
            mov byte ptr [rdx + r11 + 3] , 255


            greater_than_191:
            mov [rdx + r11] , r12
            mov [rdx + r11 + 1] , r13
            mov [rdx + r11 + 2] , r14
            mov byte ptr [rdx + r11 + 3] , 255


            ; Update resultIndex
            add R11, R10

            ; Continue inner loop
            cmp R11, rbx
            jl inner_loop

        ; Update pointers for the next row
        add rcx, rbx
        add rdx, rbx

        ; Continue outer loop
        dec r9
        jnz outer_loop
         xor rax, rax  
ret
MyProc1 endp








.code
MyProc2 PROC
    ; Arguments:
    ; rcx = sourcePixels (pointer to source pixel data)
    ; rdx = destPixels (pointer to destination pixel data)
    ; r8  = x (current x-coordinate)
    ; r9  = y (current y-coordinate)
    ; r10 = kernel (pointer to kernel array of doubles)
    ; r11d = radius (use a 32-bit register for radius)
    ; r15d = stride (stride of the image)
    ; r13d = height (height of the image)

    ; Initialize accumulators for red, green, and blue channels

    mov eax, r11d
    shl eax, 1
    mov [radiusLenght], eax
    pxor xmm0, xmm0  ; red
    pxor xmm1, xmm1  ; green
    pxor xmm2, xmm2  ; blue

    mov rax, [rsp + 40] 
    mov r10, rax 

    mov rax, [rsp + 48] 
    mov r11, rax 

    mov rax, [rsp + 56] 
    mov r15, rax 

    mov rax, [rsp + 64] 
    mov r13, rax 


    ; Loop over the kernel
    xor esi, esi  ; esi = i (loop counter)
    loop_kernel:
        ; Calculate yOffset = y + i - radius
        mov eax, r9d  ; Copy y to eax
        add eax, esi  ; Add i
        sub eax, r11d  ; Subtract radius
        ; Check if yOffset is within bounds (0 <= yOffset < height)
        cmp eax, 0
        jl skip_pixel
        cmp eax, r13d
        jge skip_pixel

        ; Calculate index = yOffset * stride + x * bytesPerPixel
        mov ebx, eax
        imul ebx, r15d
        lea ebx, [rbx + r8 * 4] ; Assuming 4 bytes per pixel (bytesPerPixel = 4)

        ; Load the weight from the kernel (double-precision)
        movsd xmm3, qword ptr [r10 + rsi * 8] ; Load kernel weight
        CVTPD2PS xmm3, xmm3 ; convert double to float


        skip_pixel:
        inc esi
        cmp esi, [radiusLenght] ; Assuming a kernel size of 5
        je loop_kernel

        ; Load source pixel values as 4 32-bit integers (RGBA format)   
        movzx r14, byte ptr[rcx+rbx+2]
        PINSRQ xmm1, r14,1
        movzx r14d, byte ptr[rcx+rbx+1]
        PINSRD xmm1, r14d,1
        PINSRB xmm1, byte ptr[rcx+rbx],0
        cvtdq2ps xmm1, xmm1 ; convert to float
        CVTPS2DQ xmm1,xmm1 ; convert from float


mov eax, r8d           ; Copy x-coordinate to eax
imul eax, 4            ; Multiply by 4 (assuming 4 bytes per pixel)
imul r9d, r15d         ; Multiply by the stride
add eax, r9d


movd ebx,xmm1
mov [rdx+rax], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+1], ebx
shufps xmm1, xmm1, 39h

movd ebx,xmm1
mov [rdx+rax+2], ebx

mov byte ptr[rdx+rax+3], 255



;movaps [eax], xmm1
;shufps xmm1, xmm1, 55h
;movd rbx,xmm1
;mov r11d, ebx
;xor rbx,rbx
;mov rbx,r11
;mov [rdx+rax], ebx


    ret
MyProc2 ENDP
end